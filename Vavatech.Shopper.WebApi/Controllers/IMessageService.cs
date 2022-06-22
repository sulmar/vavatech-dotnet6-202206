using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.WebApi.Controllers
{
    public interface IMessageService
    {
        void Send(string message, Customer customer);
    }
}
