using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.Infrastructure
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private IDictionary<int, Customer> customers;

        public FakeCustomerRepository()
        {
            customers = new Dictionary<int, Customer>
            {
                [1] = new Customer { Id = 1, FirstName = "John", LastName = "Smith", Gender = Gender.Male },
                [2] = new Customer { Id = 2, FirstName = "Ann", LastName = "Smith", Gender = Gender.Female },
                [3] = new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Gender = Gender.Male },
            };
        }

        public void Add(Customer customer)
        {
            var id = customers.Max(c=>c.Key);
            customers[++id] = customer;
        }

        public IEnumerable<Customer> Get()
        {
            return customers.Values;
        }

        public Customer Get(int id)
        {
            return customers[id];
        }

        public void Remove(int id)
        {
            customers.Remove(id);   
        }

        public void Update(Customer customer)
        {
            customers[customer.Id] = customer;
            //Remove(customer.Id);
            //customers.Add(customer.Id, customer);
        }
    }
}