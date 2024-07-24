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
        public DbSet<Urban> Urbans { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentDetail> ApartmentDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Modules)
                .WithMany(m => m.Accounts)
                .UsingEntity<AccountModule>();

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
                .HasMany(m => m.Complaints)
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

            modelBuilder.Entity<Complaint>()
                .HasMany(c => c.ComplaintDetails)
                .WithOne(cd => cd.Complaint)
                .HasForeignKey(cd => cd.ComplaintId)
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

            // Fix later
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Role)
                .WithOne(r => r.Account)
                .HasForeignKey<Role>(r => r.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            // Fix later
        }

    }
}