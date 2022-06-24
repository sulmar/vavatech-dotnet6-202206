using Microsoft.AspNetCore.Identity;
using Vavatech.AuthService.Api.Repositories;

namespace Vavatech.AuthService.Api.Models
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public bool TryAuthorize(string username, string password, out User user)
        {
            user = userRepository.Get(username);

            if (user is null)
            {
                return false;
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
