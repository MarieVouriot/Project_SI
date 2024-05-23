using Infrastructure.Entities;

namespace UserService.Users.NewFolder
{
    public interface IAuthService
    {
        bool VerifyPassword(string hashedPassword, string password);
        string GenerateToken(User user);
        void Logout(string userName);
    }

    public class AuthService : IAuthService
    {
        public bool VerifyPassword(string hashedPassword, string password)
        {
            return true;
        }

        public string GenerateToken(User user)
        {
            return "token";
        }

        public void Logout(string userName)
        {
        }
    }
}
