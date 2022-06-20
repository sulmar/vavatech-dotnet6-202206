using Bogus;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.Infrastructure
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private IDictionary<int, Customer> customers;

        public FakeCustomerRepository(Faker<Customer> faker)
        {
            customers = faker.Generate(100).ToDictionary(p=>p.Id);
        }

        public void Add(Customer customer)
        {
            var id = customers.Max(c=>c.Key);
            customer.Id = ++id;
            customers[customer.Id] = customer;
        }

        public IEnumerable<Customer> Get()
        {
            return customers.Values;
        }

        public Customer Get(int id)
        {
            if (customers.TryGetValue(id, out Customer customer))
            {
                return customer;
            }

            return null;
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