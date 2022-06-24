namespace Vavatech.AuthService.Api.Models
{
    public interface IAuthService
    {
        bool TryAuthorize(string username, string password, out User user);
    }
}
