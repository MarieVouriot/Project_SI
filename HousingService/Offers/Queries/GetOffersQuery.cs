using HousingService.Offers.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace HousingService.Offers.Queries
{
    public class GetOffersQuery : IRequest<List<OfferDTO>>
    {
        public sealed class GetOffersQueuryHandler : IRequestHandler<GetOffersQuery, List<OfferDTO>>
        {
            private ApplicationDbContext _context;
            public GetOffersQueuryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<OfferDTO>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
            {
                var offers = await _context.Offers
                    .AsNoTracking()
                    .Select(o => new OfferDTO
                    {
                        Id          = o.Id,
                        HousingId     = o.HousingId,
                        Status      = o.Status,
                        StartDate   = o.StartDate,
                        EndDate     = o.EndDate,
                        PricePerDay = o.PricePerDay
                    }).ToListAsync(cancellationToken);

                return offers ?? new List<OfferDTO>();
            }
        }
    }
}