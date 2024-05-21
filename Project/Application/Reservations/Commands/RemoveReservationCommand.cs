using MediatR;
using Project.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Reservations.Commands
{
    public class RemoveReservationCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class RemoveReservationCommandHandler : IRequestHandler<RemoveReservationCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public RemoveReservationCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveReservationCommand request, CancellationToken cancellationToken)
            {
                var reservation = await _context.Reservations.FindAsync(request.Id);

                if (reservation == null)
                {
                    throw new KeyNotFoundException("Reservation not found");
                }

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}