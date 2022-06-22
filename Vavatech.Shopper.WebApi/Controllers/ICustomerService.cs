using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.WebApi.Controllers
{
    public interface ICustomerService
    {
        Stream GeneratePdf(Customer customer);
    }
}
