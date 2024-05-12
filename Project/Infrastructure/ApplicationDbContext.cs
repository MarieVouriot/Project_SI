using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure la relation entre Reservation (Child) et User (Parent)
            builder.Entity<Reservation>()
                .HasOne(r => r.Tenant)             // Chaque réservation a un utilisateur associé (Tenant)
                .WithMany(u => u.Reservations)     // Chaque utilisateur peut avoir plusieurs réservations
                .HasForeignKey(r => r.TenantId)    // Clé étrangère dans Reservation
                .OnDelete(DeleteBehavior.Restrict); // Restriction de suppression (pas de suppression en cascade)

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=housingdb;Integrated Security=True;");
        }
    }
}
