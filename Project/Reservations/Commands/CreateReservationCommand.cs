using Infrastructure;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<ReservationDTO>
    {
        public ReservationDTO Reservation { get; }

        public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationDTO>
        {
            private readonly ApplicationDbContext _context;

            public CreateReservationCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ReservationDTO> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
            {
                var offer = await _context.Offers
                    .AsNoTracking()
                    .SingleOrDefaultAsync(o => o.Id == request.Reservation.OfferId, cancellationToken);

                if (offer == null)
                {
                    throw new Exception("Reservation linked to unknown offer.");
                }

                var reservation = new Reservation
                {
                    TenantId  = request.Reservation.TenantId,
                    OfferId   = request.Reservation.OfferId,
                    StartDate = request.Reservation.StartDate,
                    EndDate   = request.Reservation.EndDate,
                    Status    = request.Reservation.Status
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return new ReservationDTO
                {
                    Id        = reservation.Id,
                    TenantId  = reservation.TenantId,
                    OfferId   = reservation.OfferId,
                    StartDate = reservation.StartDate,
                    EndDate   = reservation.EndDate,
                    Status    = reservation.Status
                };
            }
        }
    }
}
