using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.WebApi
{
    public class CustomerDocument : IDocument
    {
        private readonly Customer customer;

        public CustomerDocument(Customer customer)
        {
            this.customer = customer;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A4);

                page.Header()
                    .Text("Hello Header");

                page.Content()
                    .Text($"Hello {customer.FirstName} {customer.LastName}!");

                page.Footer()
                    .Text("Hello Footer");
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    }
}
