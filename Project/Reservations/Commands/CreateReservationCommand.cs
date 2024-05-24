using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<ReservationDTO>
    {
        public int TenantId { get; set; }
        public int OfferId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatusEnum Status { get; set; }

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
                    .SingleOrDefaultAsync(o => o.Id == request.OfferId, cancellationToken);

                if (offer == null)
                {
                    throw new Exception("Reservation linked to unknown offer.");
                }

                var reservation = new Reservation
                {
                    TenantId  = request.TenantId,
                    OfferId   = request.OfferId,
                    StartDate = request.StartDate,
                    EndDate   = request.EndDate,
                    Status    = request.Status
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
