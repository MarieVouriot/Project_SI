using HousingService.Housings.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HousingService.Housings.Queries
{
    public class GetHousingsByIdQuery : IRequest<HousingDTO>
    {
        public int Id { get; }

        public GetHousingsByIdQuery(int id)
        {
            Id = id;
        }

        public sealed class GetHousingByIdQueryHandler : IRequestHandler<GetHousingsByIdQuery, HousingDTO>
        {
            private readonly ApplicationDbContext _context;

            public GetHousingByIdQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<HousingDTO> Handle(GetHousingsByIdQuery request, CancellationToken cancellationToken)
            {
                var housing = await _context.Housings
                    .AsNoTracking()
                    .Where(h => h.Id == request.Id)
                    .Select(h => new HousingDTO
                    {
                        Id = h.Id,
                        Address = h.Address,
                        Description = h.Description,
                        OwnerId = h.OwnerId,
                        Type = h.Type
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                return housing ?? new HousingDTO();
            }
        }
    }
}
