using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Housings.Commands
{
    public class DeleteHousingCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class DeleteHousingCommandHandler : IRequestHandler<DeleteHousingCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public DeleteHousingCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteHousingCommand request, CancellationToken cancellationToken)
            {
                var housing = await _context.Housings.FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

                if (housing == null)
                {
                    throw new KeyNotFoundException("Housing not found");
                }

                _context.Housings.Remove(housing);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}