using Vavatech.AuthService.Api.Models;

namespace Vavatech.AuthService.Api.Repositories
{
    public interface IUserRepository
    {
        User? Get(string username);
    }
}
