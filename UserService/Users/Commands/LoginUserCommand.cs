using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Users.Models;
using UserService.Users.NewFolder;

namespace UserService.Users.Commands
{
    public class LoginUserCommand : IRequest<AuthResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResult>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<AuthResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);

            if (user == null || !_authService.VerifyPassword(user.Password, request.Password))
            {
                return new AuthResult { Success = false, Message = "Invalid username or password" };
            }

            var token = _authService.GenerateToken(user);
            return new AuthResult { Success = true, Token = token };
        }
    }
}
