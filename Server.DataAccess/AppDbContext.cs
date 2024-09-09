using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        public DbSet<Urban> Urbans { get; set; }
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
        public DbSet<ModulePermission> ModulePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>();

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>();

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

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Permissions)
                .WithMany(p => p.Modules)
                .UsingEntity<ModulePermission>();

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

            modelBuilder.Entity<Account>()
                .HasMany(a => a.RefreshTokens)
                .WithOne(r => r.Account)
                .HasForeignKey(r => r.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasMany(r => r.AccessTokens)
                .WithOne(a => a.RefreshToken)
                .HasForeignKey(a => a.RtId)
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
                    new Permission() { Id = 14, PermissionName = Server.Common.Enums.Permission.UpdateNewResidentRequest.ToString() }
                );

            modelBuilder.Entity<RolePermission>()
                .HasData(
                    new RolePermission() { Id = 3, RoleId = 1, PermissionId = 13 },
                    new RolePermission() { Id = 4, RoleId = 1, PermissionId = 14 },

                    new RolePermission() { Id = 5, RoleId = 2, PermissionId = 5 },
                    new RolePermission() { Id = 6, RoleId = 2, PermissionId = 6 },
                    new RolePermission() { Id = 7, RoleId = 2, PermissionId = 7 },
                    new RolePermission() { Id = 8, RoleId = 2, PermissionId = 8 },
                    new RolePermission() { Id = 9, RoleId = 2, PermissionId = 9 },
                    new RolePermission() { Id = 10, RoleId = 2, PermissionId = 10 },
                    new RolePermission() { Id = 11, RoleId = 2, PermissionId = 11 },
                    new RolePermission() { Id = 12, RoleId = 2, PermissionId = 12 }
                );
        }

    }
}