using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    public static class InitData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                // Look for any users
                if (context.Users.Count() > 0)
                {
                    return; // DB has been seeded
                }

                // Ajoutez un utilisateur de test
                var user = new User
                {
                    FirstName = "Marie",
                    LastName = "Vouriot",
                    UserName = "marie_v",
                    Password = "mv123",
                    IsOwner = true,
                };

                context.Users.Add(user);
                context.SaveChanges();

                // Ajout de données Housing
                var housing1 = new Housing
                {
                    Address = "123 Rue de la Liberté",
                    Description = "Appartement moderne avec vue sur la ville.",
                    OwnerId = user.Id,
                    Type = HousingTypeEnum.Apartment
                };
                context.Housings.Add(housing1);
                context.SaveChanges();

                var housing2 = new Housing
                {
                    Address = "456 Avenue du Soleil",
                    Description = "Maison spacieuse avec jardin.",
                    OwnerId = user.Id,
                    Type = HousingTypeEnum.House,
                };
                context.Housings.Add(housing2);
                context.SaveChanges();

                // Ajout de données Offer
                var offer1 = new Offer
                {
                    HouseId = housing1.Id,
                    Status = OfferStatus.Available,
                    StartDate = DateTime.UtcNow.Date,
                    EndDate = DateTime.UtcNow.Date.AddDays(7),
                    PricePerDay = 100.00,
                };
                context.Offers.Add(offer1);
                context.SaveChanges();

                var offer2 = new Offer
                {
                    HouseId = housing2.Id,
                    Status = OfferStatus.Available,
                    StartDate = DateTime.UtcNow.Date,
                    EndDate = DateTime.UtcNow.Date.AddDays(14),
                    PricePerDay = 150.00,
                };
                context.Offers.Add(offer2);
                context.SaveChanges();

                var reservation1 = new Reservation
                {
                    TenantId = user.Id,
                    OfferId = offer1.Id,
                    StartDate = DateTime.UtcNow.Date.AddDays(1),
                    EndDate = DateTime.UtcNow.Date.AddDays(4),
                    Status = ReservationStatusEnum.InProgress,
                };
                context.Reservations.Add(reservation1);
                context.SaveChanges();

                var reservation2 = new Reservation
                {
                    TenantId = user.Id,
                    OfferId = offer2.Id,
                    StartDate = DateTime.UtcNow.Date.AddDays(2),
                    EndDate = DateTime.UtcNow.Date.AddDays(7),
                    Status = ReservationStatusEnum.Accepted
                };
                context.Reservations.Add(reservation2);
                context.SaveChanges();
            }
        }

    }
}
