using HousingService.Offers.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Offers.Queries
{
    public class GetOffersOfHousingQuery : IRequest<List<OfferDTO>>
    {
        public int HousingId { get; }

        public GetOffersOfHousingQuery(int housingId)
        {
            HousingId = housingId;
        }

        public sealed class GetOffersByHousingIdQueryHandler : IRequestHandler<GetOffersOfHousingQuery, List<OfferDTO>>
        {
            private readonly ApplicationDbContext _context;

            public GetOffersByHousingIdQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<OfferDTO>> Handle(GetOffersOfHousingQuery request, CancellationToken cancellationToken)
            {
                var offers = await _context.Offers
                    .AsNoTracking()
                    .Where(o => o.HousingId == request.HousingId)
                    .Select(o => new OfferDTO
                    {
                        Id = o.Id,
                        HouseId = o.HousingId,
                        StartDate = o.StartDate,
                        EndDate = o.EndDate
                    })
                    .ToListAsync(cancellationToken);

                return offers ?? new List<OfferDTO>();  
            }
        }
    }
}
