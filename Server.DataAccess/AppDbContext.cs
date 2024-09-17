using Microsoft.EntityFrameworkCore;
using Server.Common.Models;

namespace Server.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountModule> AccountModules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentDetail> DocumentDetails { get; set; }
        public DbSet<Urban> UrbanAreas { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentDetail> ApartmentDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<FormResidentRequest> FormResidentRequests { get; set; }
        public DbSet<FormResidentRequestDetail> FormResidentRequestDetails { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<AccountPermission> AccountPermissions { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Modules)
                .WithMany(m => m.Accounts)
                .UsingEntity<AccountModule>();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Documents)
                .WithOne(d => d.Account)
                .HasForeignKey(d => d.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.FormResidentRequests)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Resident)
                .WithOne(r => r.Account)
                .HasForeignKey<Resident>(r => r.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Permissions)
                .WithMany(p => p.Accounts)
                .UsingEntity<AccountPermission>();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Groups)
                .WithMany(g => g.Accounts)
                .UsingEntity<AccountGroup>();

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Tenant)
                .WithMany(t => t.Accounts)
                .HasForeignKey(a => a.TenantId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.RefreshTokens)
                .WithOne(r => r.Account)
                .HasForeignKey(r => r.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>();

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Documents)
                .WithOne(d => d.Module)
                .HasForeignKey(d => d.ModuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.FormResidentRequests)
                .WithOne(c => c.Module)
                .HasForeignKey(c => c.ModuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Buildings)
                .WithOne(b => b.Module)
                .HasForeignKey(b => b.ModuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Residents)
                .WithOne(r => r.Module)
                .HasForeignKey(r => r.ModuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasMany(d => d.DocumentDetails)
                .WithOne(dd => dd.Document)
                .HasForeignKey(dd => dd.DocumentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FormResidentRequest>()
                .HasMany(c => c.FormResidentRequestDetails)
                .WithOne(cd => cd.FormResidentRequest)
                .HasForeignKey(cd => cd.FormResidentRequestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Urban>()
                .HasMany(u => u.Buildings)
                .WithOne(b => b.Urban)
                .HasForeignKey(b => b.UrbanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Building>()
                .HasMany(b => b.Apartments)
                .WithOne(a => a.Building)
                .HasForeignKey(a => a.BuildingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Building>()
                .HasOne(b => b.Tenant)
                .WithMany(t => t.Buildings)
                .HasForeignKey(b => b.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.ApartmentDetail)
                .WithOne(ad => ad.Apartment)
                .HasForeignKey<ApartmentDetail>(ad => ad.ApartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Residents)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasMany(r => r.AccessTokens)
                .WithOne(a => a.RefreshToken)
                .HasForeignKey(a => a.RtId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Tenant)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role() { Id = 1, Name = Server.Common.Enums.Role.Admin.ToString() },
                    new Role() { Id = 2, Name = Server.Common.Enums.Role.User.ToString() }
                );

            modelBuilder.Entity<Module>()
                .HasData(
                    new Module() { Id = 1, ModuleName = Server.Common.Enums.Module.PropertyDossier.ToString() },
                    new Module() { Id = 2, ModuleName = Server.Common.Enums.Module.Form.ToString() }
                );

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithMany(p => p.Groups)
                .UsingEntity<GroupPermission>();


            // Seeds Data
            modelBuilder.Entity<Tenant>()
                .HasData(
                    new Tenant() { Id = 1, Name = "Google" },
                    new Tenant() { Id = 2, Name = "Apple" },
                    new Tenant() { Id = 3, Name = "Microsoft" },
                    new Tenant() { Id = 4, Name = "Facebook" },
                    new Tenant() { Id = 5, Name = "Amazon" }
                );

            modelBuilder.Entity<Account>()
                .HasData(
                    new Account() { Id = 1, TenantId = 1, Email = "test001@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "001", Status = Server.Common.Enums.AccountStatus.Active.ToString() },
                    new Account() { Id = 2, TenantId = 1, Email = "test002@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "002", Status = Server.Common.Enums.AccountStatus.Pending.ToString() },
                    new Account() { Id = 3, TenantId = 2, Email = "test003@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "003", Status = Server.Common.Enums.AccountStatus.Pending.ToString() },
                    new Account() { Id = 4, TenantId = 3, Email = "test004@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "004", Status = Server.Common.Enums.AccountStatus.Pending.ToString() },
                    new Account() { Id = 5, TenantId = 5, Email = "test005@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "005", Status = Server.Common.Enums.AccountStatus.Pending.ToString() }
                );

            modelBuilder.Entity<Permission>()
                .HasData(
                    new Permission() { Id = 1, PermissionName = Server.Common.Enums.Permission.CreateAccount.ToString() },
                    new Permission() { Id = 2, PermissionName = Server.Common.Enums.Permission.ReadAccount.ToString() },
                    new Permission() { Id = 3, PermissionName = Server.Common.Enums.Permission.UpdateAccount.ToString() },
                    new Permission() { Id = 4, PermissionName = Server.Common.Enums.Permission.DeleteAccount.ToString() },

                    new Permission() { Id = 5, PermissionName = Server.Common.Enums.Permission.CreatePropertyDossier.ToString() },
                    new Permission() { Id = 6, PermissionName = Server.Common.Enums.Permission.ReadPropertyDossier.ToString() },
                    new Permission() { Id = 7, PermissionName = Server.Common.Enums.Permission.UpdatePropertyDossier.ToString() },
                    new Permission() { Id = 8, PermissionName = Server.Common.Enums.Permission.DeletePropertyDossier.ToString() },

                    new Permission() { Id = 9, PermissionName = Server.Common.Enums.Permission.CreateForm.ToString() },
                    new Permission() { Id = 10, PermissionName = Server.Common.Enums.Permission.ReadForm.ToString() },
                    new Permission() { Id = 11, PermissionName = Server.Common.Enums.Permission.UpdateForm.ToString() },
                    new Permission() { Id = 12, PermissionName = Server.Common.Enums.Permission.DeleteForm.ToString() },

                    new Permission() { Id = 13, PermissionName = Server.Common.Enums.Permission.ReadAllNewResidentRequest.ToString() },
                    new Permission() { Id = 14, PermissionName = Server.Common.Enums.Permission.UpdateNewResidentRequest.ToString() },

                    new Permission() { Id = 15, PermissionName = Server.Common.Enums.Permission.CanViewAllForms.ToString() },
                    new Permission() { Id = 16, PermissionName = Server.Common.Enums.Permission.CanViewTenantForms.ToString() },
                    new Permission() { Id = 17, PermissionName = Server.Common.Enums.Permission.CanViewOwnForms.ToString() },

                    new Permission() { Id = 18, PermissionName = Server.Common.Enums.Permission.CreateGroup.ToString() },
                    new Permission() { Id = 19, PermissionName = Server.Common.Enums.Permission.ReadGroup.ToString() },
                    new Permission() { Id = 20, PermissionName = Server.Common.Enums.Permission.UpdateGroup.ToString() },
                    new Permission() { Id = 21, PermissionName = Server.Common.Enums.Permission.DeleteGroup.ToString() }
                );
        }

    }
}