using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Models.SearchCriterias;

namespace Vavatech.Shopper.Models.Repositories;

public interface IProductRepository : IEntityRepository<Product>
{
    IEnumerable<Product> Get(ProductSearchCriteria searchCriteria);
}
