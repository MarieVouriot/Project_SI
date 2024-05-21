using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Offers.Models;


namespace ReservationService.Offers.Queries
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
                        HouseId     = o.HouseId,
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