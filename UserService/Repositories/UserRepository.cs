using Azure.Core;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Users.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(string lastName, string firstName, string userName, string password, bool isOwner, CancellationToken cancellationToken);
    }

    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task AddUserAsync(string lastName, string firstName, string userName, string password, bool isOwner, CancellationToken cancellationToken)
        {
            var userToAdd = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password,
                IsOwner = isOwner,
            };
            await context.Users.AddAsync(userToAdd, cancellationToken);
            await context.SaveChangesAsync();
        }
    }
}
