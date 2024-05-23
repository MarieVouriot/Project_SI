using MediatR;
using Microsoft.AspNetCore.Authentication;
using UserService.Users.NewFolder;

namespace UserService.Users.Commands
{
    public class LogoutUserCommand : IRequest
    {
        public string UserName { get; set; }
    }

    /*public class LogoutUserCommandHandler : IRequestHandler<>
    {
        private readonly IAuthService _authService;

        public LogoutUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            _authService.Logout(request.UserName);
            return await Task.FromResult(Unit.Value);
        }
    }*/
}
