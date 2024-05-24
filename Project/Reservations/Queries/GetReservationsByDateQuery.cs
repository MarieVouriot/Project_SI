using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Queries
{
    public class GetReservationsByDateQuery : IRequest<List<ReservationDTO>>
    {
        public DateTime? Date { get; set; }
        public byte? Status { get; set; }

        public sealed class GetReservationsQueryHandler : IRequestHandler<GetReservationsByDateQuery, List<ReservationDTO>>
        {
            private readonly ApplicationDbContext _context;

            public GetReservationsQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<ReservationDTO>> Handle(GetReservationsByDateQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Reservations.AsNoTracking().AsQueryable();

                if (request.Date.HasValue)
                {
                    query = query.Where(r => r.StartDate <= request.Date && r.EndDate >= request.Date);
                }

                if (request.Status.HasValue)
                {
                    query = query.Where(r => (byte)r.Status == request.Status);
                }

                var reservations = await query
                    .Select(r => new ReservationDTO
                    {
                        Id        = r.Id,
                        TenantId  = r.TenantId,
                        OfferId   = r.OfferId,
                        StartDate = r.StartDate,
                        EndDate   = r.EndDate,
                        Status    = r.Status
                    }).ToListAsync(cancellationToken);

                return reservations;
            }
        }
    }
}
