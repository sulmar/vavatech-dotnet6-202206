using Vavatech.Shopper.Models.SearchCriterias;

namespace Vavatech.Shopper.Models.Repositories;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    Customer Get(string lastName);
    IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);
}
