using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationService.Reservations.Models;

namespace ReservationService.Reservations.Queries
{
    public class GetReservationsQuery : IRequest<IEnumerable<ReservationDTO>>
    {
        public DateTime? Date { get; }
        public string Status { get; }

        public GetReservationsQuery(DateTime? date, string status)
        {
            Date = date;
            Status = status;
        }

        public sealed class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, IEnumerable<ReservationDTO>>
        {
            private readonly ApplicationDbContext _context;

            public GetReservationsQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ReservationDTO>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Reservations.AsNoTracking().AsQueryable();

                if (request.Date.HasValue)
                {
                    query = query.Where(r => r.StartDate <= request.Date && r.EndDate >= request.Date);
                }

                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(r => r.Status.ToString().Equals(request.Status, StringComparison.OrdinalIgnoreCase));
                }

                var reservations = await query
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
