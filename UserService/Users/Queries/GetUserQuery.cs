using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Users.Models;

namespace UserService.Users.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
        public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
        {
            private ApplicationDbContext _context;
            public GetUserQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .AsNoTracking()
                    .Where(u => u.Id == request.Id)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Password = u.Password,
                        IsOwner = u.IsOwner,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                return user ?? new UserDto();
            }

        }

    }
}
