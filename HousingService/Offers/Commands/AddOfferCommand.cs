using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Offers.Commands
{
    public class AddOfferCommand : IRequest<Unit>
    {
        public int HouseId { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerDay { get; set; }
        
        public sealed class AddOfferCommandHandler : IRequestHandler<AddOfferCommand, Unit>
        {
            private ApplicationDbContext _context;
            public AddOfferCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddOfferCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var housing = await _context.Housings.AsNoTracking().FirstOrDefaultAsync(h => h.Id == request.HouseId, cancellationToken);

                    if (housing == null)
                    {
                        throw new Exception("No housing found.");
                    }

                    var offer = new Offer
                    {
                        HousingId     = housing.Id,
                        Status      = request.Status,
                        StartDate   = request.StartDate,
                        EndDate     = request.EndDate,
                        PricePerDay = request.PricePerDay,
                    };

                    await _context.Offers.AddAsync(offer, cancellationToken);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw new Exception("Cannot add offer");
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }

                return Unit.Value;
            }
        }
    }
}