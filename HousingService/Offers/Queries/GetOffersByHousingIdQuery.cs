using HousingService.Offers.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HousingService.Offers.Queries
{
    public class GetOffersByHousingIdQuery : IRequest<List<OfferDTO>>
    {
        public int HousingId { get; }

        public GetOffersByHousingIdQuery(int housingId)
        {
            HousingId = housingId;
        }

        public sealed class GetOffersByHousingIdQueryHandler : IRequestHandler<GetOffersByHousingIdQuery, List<OfferDTO>>
        {
            private readonly ApplicationDbContext _context;

            public GetOffersByHousingIdQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<OfferDTO>> Handle(GetOffersByHousingIdQuery request, CancellationToken cancellationToken)
            {
                var offers = await _context.Offers
                    .AsNoTracking()
                    .Where(o => o.HouseId == request.HousingId)
                    .Select(o => new OfferDTO
                    {
                        Id = o.Id,
                        HouseId = o.HouseId,
                        StartDate = o.StartDate,
                        EndDate = o.EndDate
                    })
                    .ToListAsync(cancellationToken);

                return offers ?? new List<OfferDTO>();  
            }
        }
    }
}
