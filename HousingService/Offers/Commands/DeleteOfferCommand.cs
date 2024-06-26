﻿using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HousingService.Offers.Commands
{
    public class DeleteOfferCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, Unit>
        {
            private readonly ApplicationDbContext _context;

            public DeleteOfferCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
            {
                var offer = await _context.Offers.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

                if (offer == null)
                {
                    throw new KeyNotFoundException("Offer not found");
                }

                _context.Offers.Remove(offer);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}