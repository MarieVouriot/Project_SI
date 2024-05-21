﻿using MediatR;
using Project.Infrastructure;
using Project.Enums;
using Project.Infrastructure.Entities;


namespace Project.Application.Housings.Commands
{
    public sealed class AddHousingCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public HousingTypeEnum Type { get; set; }

        public sealed class AddHousingCommandHandler : IRequestHandler<AddHousingCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public AddHousingCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddHousingCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var housing = new Infrastructure.Entities.Housing
                    {
                        Address = request.Address,
                        Description = request.Description,
                        OwnerId = request.OwnerId,
                        Type = request.Type
                    };

                    await _context.Housings.AddAsync(housing, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    throw new Exception("Cannot add housing");
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }

                return Unit.Value;
            }
        }
    }
}