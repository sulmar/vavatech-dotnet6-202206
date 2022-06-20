using Vavatech.Shopper.Models.SearchCriterias;

namespace Vavatech.Shopper.Models.Repositories;
public interface ICustomerRepository
{
    IEnumerable<Customer> Get();
    Customer Get(int id);
    void Add(Customer customer);
    void Update(Customer customer);
    void Remove(int id);

    Customer Get(string lastName);
    IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);
}

public interface IOrderRepository
{
    IEnumerable<Order> Get(int customerId);
}
