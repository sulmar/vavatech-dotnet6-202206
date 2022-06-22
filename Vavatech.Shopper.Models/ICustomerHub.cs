using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.Domain
{
    public interface ICustomerHub
    {
        Task SendAddedCustomer(Customer customer);
        Task Ping();
    }

    
}
