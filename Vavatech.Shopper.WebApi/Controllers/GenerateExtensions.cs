using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.WebApi.Controllers
{
    public static class GenerateExtensions
    {
        public static void GeneratePdf(Customer customer)
        {
            var document = new CustomerDocument(customer);

            Stream stream = new MemoryStream();

            if (customer.Gender == Gender.Male)
                throw new ApplicationException("for only woman");

            document.GeneratePdf(stream);

            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
