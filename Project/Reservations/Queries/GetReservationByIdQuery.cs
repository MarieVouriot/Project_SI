using Infrastructure;
using MediatR;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Queries
{
    public class GetReservationByIdQuery : IRequest<ReservationDTO>
    {
        public int Id { get; }

        public GetReservationByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDTO>
    {
        private readonly ApplicationDbContext _context;

        public GetReservationByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDTO> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(request.Id);
            if (reservation == null) return null;

            return new ReservationDTO
            {
                Id = reservation.Id,
                TenantId = reservation.TenantId,
                OfferId = reservation.OfferId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status
            };
        }
    }
}
