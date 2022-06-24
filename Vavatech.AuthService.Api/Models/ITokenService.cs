namespace Vavatech.AuthService.Api.Models
{
    public interface ITokenService
    {
        string Create(User user);
    }
}
