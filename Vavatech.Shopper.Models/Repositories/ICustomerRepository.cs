namespace Vavatech.Shopper.Models.Repositories;
public interface ICustomerRepository
{
    IEnumerable<Customer> Get();
    Customer Get(int id);
    void Add(Customer customer);
    void Update(Customer customer);
    void Remove(int id);
}
