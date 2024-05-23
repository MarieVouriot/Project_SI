using Infrastructure;
using MediatR;
using ReservationService.Reservations.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationService.Reservations.Queries
{
    public class GetAllReservationsQuery : IRequest<IEnumerable<ReservationDTO>>
    {
    }

    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, IEnumerable<ReservationDTO>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllReservationsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReservationDTO>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = _context.Reservations
                .Select(r => new ReservationDTO
                {
                    Id = r.Id,
                    TenantId = r.TenantId,
                    OfferId = r.OfferId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Status = r.Status
                })
                .ToList();

            return reservations;
        }
    }
}
