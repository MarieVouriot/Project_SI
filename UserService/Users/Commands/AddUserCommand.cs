using Infrastructure;
using Infrastructure.Entities;
using MediatR;
using UserService.Repositories;

namespace UserService.Users.Commands
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
            private IUserRepository _userRepository;
            public AddUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
                _userRepository = new UserRepository(context);
            }

            public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                try
                {
                    await _userRepository.AddUserAsync(request.LastName, request.FirstName, request.UserName, request.Password, request.IsOwner, cancellationToken);
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
