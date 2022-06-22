using Bogus;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));
            RuleFor(p => p.Size, f => f.PickRandom<Size>());
        }
    }
}
