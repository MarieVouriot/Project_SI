using HousingService.Housings.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Housings.Queries
{
    public class GetHousingByIdQuery : IRequest<HousingDTO>
    {
        public int Id { get; }

        public sealed class GetHousingByIdQueryHandler : IRequestHandler<GetHousingByIdQuery, HousingDTO>
        {
            private readonly ApplicationDbContext _context;

            public GetHousingByIdQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<HousingDTO> Handle(GetHousingByIdQuery request, CancellationToken cancellationToken)
            {
                var housing = await _context.Housings
                    .AsNoTracking()
                    .Where(h => h.Id == request.Id)
                    .Select(h => new HousingDTO
                    {
                        Id          = h.Id,
                        Address     = h.Address,
                        Description = h.Description,
                        OwnerId     = h.OwnerId,
                        Type        = h.Type
                    }).FirstOrDefaultAsync(cancellationToken);

                if (housing == null)
                {
                    throw new Exception("No housing found.");
                }

                return housing;
            }
        }
    }
}
