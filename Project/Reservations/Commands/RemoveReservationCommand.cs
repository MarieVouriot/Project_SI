﻿using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ReservationService.Reservations.Commands
{
    public class RemoveReservationCommand : IRequest<bool>
    {
        public int Id { get; }

        public RemoveReservationCommand(int id)
        {
            Id = id;
        }
    }

    public class RemoveReservationCommandHandler : IRequestHandler<RemoveReservationCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public RemoveReservationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}