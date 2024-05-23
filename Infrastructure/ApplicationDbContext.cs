using System.Reflection.Emit;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuration de l'entité User
            builder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(50);
            });

            // Configuration de l'entité Housing
            builder.Entity<Housing>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Address).IsRequired().HasMaxLength(100);
                entity.Property(h => h.Description).HasMaxLength(500);

                // Foreign Key - OwnerId
                entity.HasOne(h => h.Owner)
                    .WithMany(u => u.Housings)
                    .HasForeignKey(h => h.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Housing_User");
            });

            // Configuration de l'entité Offer
            builder.Entity<Offer>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Status).IsRequired();
                entity.Property(o => o.StartDate).IsRequired();
                entity.Property(o => o.EndDate).IsRequired();
                entity.Property(o => o.PricePerDay).IsRequired().HasColumnType("decimal(18,2)");

                // Foreign Key - HousingId
                entity.HasOne(o => o.Housing)
                    .WithMany(h => h.Offers)
                    .HasForeignKey(o => o.HousingId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Offer_Housing");
            });

            // Configuration de l'entité Reservation
            builder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.StartDate).IsRequired();
                entity.Property(r => r.EndDate).IsRequired();
                entity.Property(r => r.Status).IsRequired();

                // Foreign Key - TenantId
                entity.HasOne(r => r.Tenant)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.TenantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Reservation_User");

                // Foreign Key - OfferId
                entity.HasOne(r => r.Offer)
                    .WithMany(o => o.Reservations)
                    .HasForeignKey(r => r.OfferId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Reservation_Offer");
            });
        }
    }
}
