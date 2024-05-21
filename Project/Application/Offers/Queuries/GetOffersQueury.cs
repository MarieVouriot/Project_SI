﻿using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Offers.Models;


namespace Project.Application.Offers.Queries
{
    public class GetOffersQueury : IRequest<List<OfferDTO>>
    {
        public sealed class GetOffersQueuryHandler : IRequestHandler<GetOffersQueury, List<OfferDTO>>
        {
            private ApplicationDbContext _context;
            public GetOffersQueuryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<OfferDTO>> Handle(GetOffersQueury request, CancellationToken cancellationToken)
            {
                var offers = await _context.Offers
                    .AsNoTracking()
                    .Select(o => new OfferDTO
                    {
                        Id          = o.Id,
                        HouseId     = o.HouseId,
                        Status      = o.Status,
                        StartDate   = o.StartDate,
                        EndDate     = o.EndDate,
                        PricePerDay = o.PricePerDay
                    }).ToListAsync(cancellationToken);

                return offers ?? new List<OfferDTO>();
            }
            
        }
    }
}