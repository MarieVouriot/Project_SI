using Microsoft.EntityFrameworkCore;
using Project.Infrastructure;
using System.Globalization;

namespace Project.Entities
{
    public static class InitData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                // Look for any movies.
                if (context.Users.Count() > 0)
                {
                    return;   // DB has been seeded
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
            }
        }
    }
}
