using MediatR;
using Project.Infrastructure;
using Project.Infrastructure.Entities;

namespace Project.Application.Users.Commands
{
    public sealed class AddUserCommand : IRequest<Unit>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsOwner { get; set; }

        public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
        {
            private ApplicationDbContext _context;
            public AddUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    var user = new User
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        UserName = request.UserName,
                        Password = request.Password,
                        IsOwner = request.IsOwner,
                    };

                    // TODO : add to database
                    await _context.Users.AddAsync(user, cancellationToken);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw new Exception("Cannot add user");
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
