using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Queries
{
    public class GetReservationByIdQuery : IRequest<ReservationDTO>
    {
        public int Id { get; }

        public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDTO>
        {
            private readonly ApplicationDbContext _context;

            public GetReservationByIdQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ReservationDTO> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
            {
                var reservation = await _context.Reservations
                    .AsNoTracking()
                    .Select(r => new ReservationDTO
                    {
                        Id = r.Id,
                        TenantId = r.TenantId,
                        OfferId = r.OfferId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        Status = r.Status
                    }).SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                if (reservation == null)
                {
                    throw new Exception("No reservation found");
                }
                return reservation;
            }
        }
    }
}
