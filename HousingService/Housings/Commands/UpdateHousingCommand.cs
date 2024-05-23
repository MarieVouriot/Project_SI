using Infrastructure;
using Infrastructure.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Housings.Commands
{
    public sealed class UpdateHousingCommand : IRequest<Unit>
    {
        public int HousingId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public HousingTypeEnum Type { get; set; }

        public sealed class UpdateHousingsCommandHandler : IRequestHandler<UpdateHousingCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public UpdateHousingsCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateHousingCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var housing = await _context.Housings.FirstOrDefaultAsync(h => h.Id == request.HousingId, cancellationToken);
                    if (housing == null)
                    {
                        throw new Exception("Housing not found");
                    }

                    housing.Address     = request.Address;
                    housing.Description = request.Description;
                    housing.OwnerId     = request.OwnerId;
                    housing.Type        = request.Type;

                    _context.Housings.Update(housing);
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
