using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Server.BusinessLogic.Interfaces;
using Server.BusinessLogic.Services;
using Server.DataAccess;
using Server.DataAccess.Interfaces;
using Server.DataAccess.Repositories;
using Server.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option
    => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Headers["access_token"];
                if (string.IsNullOrEmpty(accessToken) == false)
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtBearer:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtBearer:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtBearer:Key"] ?? throw new Exception("Unexpected Error"))),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(options => true)
                    .AllowCredentials()
            );
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddScoped<IAuthLibraryService, AuthLibraryService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICookieHelper, CookieHelper>();
builder.Services.AddScoped<IPropertyDossierService, PropertyDossierService>();
builder.Services.AddScoped<IPropertyDossierRepository, PropertyDossierRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IAuthorizeRepository, AuthorizeRepository>();
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IDocRepository, DocRepository>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<FailDetectionHub>("/api/fail-detection-hub");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Applies any pending migrations
}


app.Run();
