using MediatR;
using Project.Enums;
using Project.Infrastructure;
using Project.Infrastructure.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Reservations.Commands
{
    public class AddReservationCommand : IRequest<Unit>
    {
        public int TenantId { get; set; }
        public int OfferId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatusEnum Status { get; set; }

        public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public AddReservationCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddReservationCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var reservation = new Reservation
                    {
                        TenantId = request.TenantId,
                        OfferId = request.OfferId,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        Status = request.Status
                    };

                    await _context.Reservations.AddAsync(reservation, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    throw new Exception("Cannot add reservation");
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