using Vavatech.Shopper.Models;
using QuestPDF.Fluent;

namespace Vavatech.Shopper.WebApi.Controllers
{
    public class CustomerService : ICustomerService
    {
        public Stream GeneratePdf(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var document = new CustomerDocument(customer);

            Stream stream = new MemoryStream();

            if (customer.Gender == Gender.Male)
                throw new ApplicationException("for only woman");

            document.GeneratePdf(stream);            

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
