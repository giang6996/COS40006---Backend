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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.FormResidentRequests)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

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
                .OnDelete(DeleteBehavior.Cascade);

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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Tenant)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
               .HasMany(d => d.DocumentDetails)
               .WithOne(dd => dd.Document)
               .HasForeignKey(dd => dd.DocumentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Apartment)
                .WithMany()
                .HasForeignKey(d => d.ApartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Building)
                .WithMany()
                .HasForeignKey(d => d.BuildingId)
                .IsRequired()
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

            //modelBuilder.Entity<Account>()
            //    .HasData(
            //        new Account() { Id = 1, TenantId = 1, Email = "test001@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "001", Status = Server.Common.Enums.AccountStatus.Active.ToString() },
            //        new Account() { Id = 2, TenantId = 1, Email = "test002@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "002", Status = Server.Common.Enums.AccountStatus.Pending.ToString() },
            //        new Account() { Id = 3, TenantId = 2, Email = "test003@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "003", Status = Server.Common.Enums.AccountStatus.Pending.ToString() },
            //        new Account() { Id = 4, TenantId = 3, Email = "test004@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "004", Status = Server.Common.Enums.AccountStatus.Pending.ToString() },
            //        new Account() { Id = 5, TenantId = 5, Email = "test005@gmail.com", Password = "$2a$11$eaCV0/gsB6YuF3e86QZ2CeUAb2dK7L1rSfuNxNiVPyGpkEVqzj6s.", FirstName = "test", LastName = "005", Status = Server.Common.Enums.AccountStatus.Pending.ToString() }
            //    );

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
            modelBuilder.Entity<Urban>()
                .HasData(
                new Urban { Id = 1, UrbanAddress = "Urban Area 1" },
                new Urban { Id = 2, UrbanAddress = "Urban Area 2" },
                new Urban { Id = 3, UrbanAddress = "Urban Area 3" }
            );

            modelBuilder.Entity<Building>()
                .HasData(
                new Building
                {
                    Id = 1,
                    TenantId = 1,
                    UrbanId = 1,
                    ModuleId = 1,
                    NumberFloor = 10,
                    BuildingName = "Sunrise Tower",
                    BuildingAddress = "123 Main St"
                },
                new Building
                {
                    Id = 2,
                    TenantId = 2,
                    UrbanId = 2,
                    ModuleId = 1,
                    NumberFloor = 15,
                    BuildingName = "Moonlight Apartments",
                    BuildingAddress = "456 Elm St"
                },
                new Building { 
                    Id = 3, 
                    TenantId = 3, 
                    UrbanId = 3, 
                    ModuleId = 1, 
                    NumberFloor = 8, 
                    BuildingName = "Building C", 
                    BuildingAddress = "789 Elm St" 
                }
            );

            // Seed data for RolePermissions (associating Admin role with permissions)
            modelBuilder.Entity<RolePermission>()
                .HasData(
                    new RolePermission { Id = 1, RoleId = 1, PermissionId = 1 }, // Admin can CreateAccount
                    new RolePermission { Id = 2, RoleId = 1, PermissionId = 2 }, // Admin can ReadAccount
                    new RolePermission { Id = 3, RoleId = 1, PermissionId = 3 }, // Admin can UpdateAccount
                    new RolePermission { Id = 4, RoleId = 1, PermissionId = 4 }, // Admin can DeleteAccount
                    new RolePermission { Id = 5, RoleId = 1, PermissionId = 13 }, // Admin can ReadAllNewResidentRequest
                    new RolePermission { Id = 6, RoleId = 1, PermissionId = 14 }  // Admin can UpdateNewResidentRequest
                );

            //// Seed data for Apartments
            //modelBuilder.Entity<Apartment>().HasData(
            //    new Apartment
            //    {
            //        Id = 1,
            //        BuildingId = 1,
            //        Available = "Yes",
            //        RoomNumber = 101
            //    },
            //    new Apartment
            //    {
            //        Id = 2,
            //        BuildingId = 1,
            //        Available = "No",
            //        RoomNumber = 102
            //    },
            //    new Apartment
            //    {
            //        Id = 3,
            //        BuildingId = 2,
            //        Available = "Yes",
            //        RoomNumber = 201
            //    }
            //);

            //// Seed data for ApartmentDetails
            //modelBuilder.Entity<ApartmentDetail>().HasData(
            //    new ApartmentDetail
            //    {
            //        Id = 1,
            //        ApartmentId = 1,
            //        Type = "Studio",
            //        NumBedroom = 1,
            //        NumBathroom = 1,
            //        Size = 500.0
            //    },
            //    new ApartmentDetail
            //    {
            //        Id = 2,
            //        ApartmentId = 2,
            //        Type = "One Bedroom",
            //        NumBedroom = 1,
            //        NumBathroom = 1,
            //        Size = 650.0
            //    },
            //    new ApartmentDetail
            //    {
            //        Id = 3,
            //        ApartmentId = 3,
            //        Type = "Two Bedroom",
            //        NumBedroom = 2,
            //        NumBathroom = 2,
            //        Size = 900.0
            //    }
            //);

            // Seed data for Apartments
            var apartments = new List<Apartment>();
            var apartmentDetails = new List<ApartmentDetail>();

            for (int i = 1; i <= 100; i++)
            {
                int buildingId = (i % 3) + 1; // Distribute apartments across 3 buildings
                apartments.Add(new Apartment
                {
                    Id = i,
                    BuildingId = buildingId,
                    Available = i % 2 == 0 ? "Yes" : "No", // Alternate availability
                    RoomNumber = 100 + i
                });

                apartmentDetails.Add(new ApartmentDetail
                {
                    Id = i,
                    ApartmentId = i,
                    Type = (i % 3 == 0) ? "Penthouse" : (i % 2 == 0) ? "Studio" : "One Bedroom",
                    NumBedroom = (i % 3) + 1, // Randomize between 1, 2, and 3 bedrooms
                    NumBathroom = (i % 2) + 1, // Alternate between 1 and 2 bathrooms
                    Size = 50 + (i % 3) * 25 // Size ranges from 50, 75, to 100 sq. meters
                });
            }

            modelBuilder.Entity<Apartment>().HasData(apartments);
            modelBuilder.Entity<ApartmentDetail>().HasData(apartmentDetails);

        }

    }
}