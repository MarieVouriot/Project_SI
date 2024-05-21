using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure;
using Project.Infrastructure.Entities;

namespace Project.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
        {
            private ApplicationDbContext _context;
            public UpdateUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var user = await _context.Users.Where(u => u.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                    if(user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(request.FirstName)) user.FirstName = request.FirstName;
                        if (!string.IsNullOrWhiteSpace(request.LastName)) user.LastName = request.LastName;
                        if (!string.IsNullOrWhiteSpace(request.UserName)) user.UserName = request.UserName;
                        if (!string.IsNullOrWhiteSpace(request.Password)) user.Password = request.Password;
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    throw new Exception("Cannot update user");
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;
                }

                return Unit.Value;
            }
        }
    }
}
