using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UserService.Users.Commands
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
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                    if (user != null)
                    {
                        bool hasChanged = false;

                        if (!string.IsNullOrWhiteSpace(request.FirstName) && user.FirstName != request.FirstName)
                        {
                            user.FirstName = request.FirstName;
                            hasChanged = true;
                        }
                        if (!string.IsNullOrWhiteSpace(request.LastName) && user.LastName != request.LastName)
                        {
                            user.LastName = request.LastName;
                            hasChanged = true;
                        }
                        if (!string.IsNullOrWhiteSpace(request.UserName) && user.UserName != request.UserName)
                        {
                            user.UserName = request.UserName;
                            hasChanged = true;
                        }
                        if (!string.IsNullOrWhiteSpace(request.Password) && user.Password != request.Password)
                        {
                            user.Password = request.Password;
                            hasChanged = true;
                        }

                        if (hasChanged)
                        {
                            _context.Update(user);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    }
                    else
                    {
                        throw new Exception("User not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot update user", ex);
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
