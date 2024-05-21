using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Users.Models;

namespace UserService.Users.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
        {
            private ApplicationDbContext _context;
            public GetUsersQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _context.Users
                    .AsNoTracking()
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Password = u.Password,
                        IsOwner = u.IsOwner,
                    }).ToListAsync(cancellationToken);

                return users ?? new List<UserDto>();
            }
        }
    }
}
