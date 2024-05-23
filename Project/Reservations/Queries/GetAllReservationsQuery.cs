using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Queries
{
    public class GetAllReservationsQuery : IRequest<List<ReservationDTO>>
    {
        public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<ReservationDTO>>
        {
            private readonly ApplicationDbContext _context;

            public GetAllReservationsQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<ReservationDTO>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
            {
                var reservations = await _context.Reservations
                    .Select(r => new ReservationDTO
                    {
                        Id = r.Id,
                        TenantId = r.TenantId,
                        OfferId = r.OfferId,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        Status = r.Status
                    }).ToListAsync(cancellationToken);

                return reservations;
            }
        }
    }
}
