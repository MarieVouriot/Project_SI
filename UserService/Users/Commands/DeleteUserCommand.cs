using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UserService.Users.Commands
{
    public sealed class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
        {
            private ApplicationDbContext _context;
            public DeleteUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var userToDelete = await _context.Users.Where(u => request.Id == u.Id).FirstOrDefaultAsync(cancellationToken);
                    if (userToDelete != null)
                    {
                        _context.Users.Remove(userToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                catch
                {
                    throw new Exception("Cannot delete user");
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
