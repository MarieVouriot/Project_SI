using HousingService.Housings.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Housings.Queries
{
    public class GetHousingsQuery : IRequest<List<HousingDTO>>
    {
        public sealed class GetHousingsQueuryHandler : IRequestHandler<GetHousingsQuery, List<HousingDTO>>
        {
            private ApplicationDbContext _context;
            public GetHousingsQueuryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<HousingDTO>> Handle(GetHousingsQuery request, CancellationToken cancellationToken)
            {
                var housings = await _context.Housings
                    .AsNoTracking()
                    .Select(h => new HousingDTO
                    {
                        Id = h.Id,
                        Address = h.Address,
                        Description = h.Description,
                        OwnerId = h.OwnerId,
                        Type = h.Type
                    }).ToListAsync(cancellationToken);

                return housings ?? new List<HousingDTO>();
            }

        }
    }
}