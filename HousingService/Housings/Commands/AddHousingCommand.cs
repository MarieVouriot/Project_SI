using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace HousingService.Housings.Commands
{
    public sealed class AddHousingCommand : IRequest<Unit>
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public HousingTypeEnum Type { get; set; }

        public sealed class AddHousingCommandHandler : IRequestHandler<AddHousingCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public AddHousingCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddHousingCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var owner = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u  => u.Id == request.OwnerId, cancellationToken);
                    
                    if (owner == null || !owner.IsOwner)
                    {
                        throw new Exception("User not found");
                    }

                    var housing = new Housing
                    {
                        Address     = request.Address,
                        Description = request.Description,
                        OwnerId     = request.OwnerId,
                        Type        = request.Type
                    };

                    await _context.Housings.AddAsync(housing, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    throw new Exception("Cannot add housing");
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