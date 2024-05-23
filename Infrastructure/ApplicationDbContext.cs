using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
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

            // Configure la relation entre Offer (Child) et Housing (Parent)
            builder.Entity<Offer>()
                .HasOne(o => o.Housing)             // Chaque offre a un logement associé
                .WithMany(h => h.Offers)            // Chaque logement peut avoir plusieurs offres
                .HasForeignKey(o => o.HouseId)     // Clé étrangère dans Offer
                .OnDelete(DeleteBehavior.Restrict); // Restriction de suppression (pas de suppression en cascade)
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=housingdb;Integrated Security=True;");
        //}
    }
}
