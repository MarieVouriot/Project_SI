using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Reservations.Models;


namespace Project.Application.Reservations.Queries
{
    public class GetReservationsQueury : IRequest<ReservationDTO>
    {
        public sealed class GetReservationsQueuryHandler : IRequestHandler<GetReservationsQueury, ReservationDTO>
        {
            private ApplicationDbContext _context;

            public GetReservationsQueuryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ReservationDTO> Handle(GetReservationsQueury request, CancellationToken cancellationToken)
            {
                var reservation = await _context.Reservations
                    .AsNoTracking()
                    .Select(r => new ReservationDTO
                    {
                        Id        = r.Id,
                        TenantId  = r.TenantId,
                        OfferId   = r.OfferId,
                        StartDate = r.StartDate,
                        EndDate   = r.EndDate,
                        Status    = r.Status
                    }).FirstOrDefaultAsync(cancellationToken);

                return reservation ?? new ReservationDTO();
            }
        }
    }
}