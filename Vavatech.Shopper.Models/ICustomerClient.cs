using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.Domain
{
    public interface ICustomerClient
    {
        Task NewCustomer(Customer customer);
        Task Pong();
    }    
}
