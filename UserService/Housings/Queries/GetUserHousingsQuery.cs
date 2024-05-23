using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Housings.Models;
using Infrastructure;

namespace UserService.Users.Queries
{
    public class GetUserHousingsQuery : IRequest<List<HousingDTO>>
    {
        public int UserId { get; set; }
    }
    public class GetUserHousingsQueryHandler : IRequestHandler<GetUserHousingsQuery, List<HousingDTO>>
    {
        private readonly ApplicationDbContext _context;

        public GetUserHousingsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HousingDTO>> Handle(GetUserHousingsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Housings)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            var housings = user.Housings.Select(h => new HousingDTO
            {
                Id = h.Id,
                Address = h.Address,
                Description = h.Description,
                OwnerId = h.OwnerId,
                Type = h.Type
            }).ToList();

            return housings;
        }
    }
}

