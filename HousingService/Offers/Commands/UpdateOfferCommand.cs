using Infrastructure;
using Infrastructure.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Offers.Commands
{
    public sealed class UpdateOfferCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerDay { get; set; }

        public sealed class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public UpdateOfferCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var offer = await _context.Offers.FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);
                    if (offer == null)
                    {
                        throw new Exception("Offer not found");
                    }

                    offer.Status      = request.Status;
                    offer.StartDate   = request.StartDate;
                    offer.EndDate     = request.EndDate;
                    offer.PricePerDay = request.PricePerDay;

                    _context.Offers.Update(offer);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    throw new Exception("Cannot update housing");
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
