using Vavatech.AuthService.Api.Repositories;

namespace Vavatech.AuthService.Api.Models
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool TryAuthorize(string username, string password, out User user)
        {
            user = userRepository.Get(username);

            if (user is null)
            {
                return false;
            }

            return user.HashedPassword == password;
        }
    }
}
