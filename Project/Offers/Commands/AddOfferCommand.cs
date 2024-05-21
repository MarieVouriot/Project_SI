using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Enums;
using MediatR;

namespace ReservationService.Offers.Commands
{
    public class AddOfferCommand : IRequest<Unit>
    {
        public int Id { get; set; }
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
                    var offer = new Offer
                    {
                        Id = request.Id,
                        HouseId = request.HouseId,
                        Status = request.Status,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        PricePerDay = request.PricePerDay
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