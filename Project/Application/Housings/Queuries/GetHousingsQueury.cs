using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Housing.Models;


namespace Project.Application.Housing.Queries
{
    public class GetHousingsQueury : IRequest<List<HousingDTO>>
    {
        public sealed class GetHousingsQueuryHandler : IRequestHandler<GetHousingsQueury, List<HousingDTO>>
        {
            private ApplicationDbContext _context;
            public GetHousingsQueuryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<HousingDTO>> Handle(GetHousingsQueury request, CancellationToken cancellationToken)
            {
                var housings = await _context.Housings
                    .AsNoTracking()
                    .Select(h => new HousingDTO
                    {
                        Id          = h.Id,
                        Address     = h.Address,
                        Description = h.Description,
                        OwnerId     = h.OwnerId,
                        Type        = h.Type
                    }).ToListAsync(cancellationToken);

                return housings ?? new List<HousingDTO>();
            }
            
        }
    }
}