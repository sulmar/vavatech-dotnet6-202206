using Bogus;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;
using Vavatech.Shopper.Models.SearchCriterias;

namespace Vavatech.Shopper.Infrastructure
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private IDictionary<int, Customer> customers;

        public FakeCustomerRepository(Faker<Customer> faker)
        {
            customers = faker.Generate(100).ToDictionary(p => p.Id);
        }

        public void Add(Customer customer)
        {
            var id = customers.Max(c => c.Key);
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

        public Customer Get(string lastName)
        {
            return customers.Values.SingleOrDefault(c => c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            var query = customers.Values.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                query = query.Where(c => c.FirstName.Equals(searchCriteria.FirstName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchCriteria.LastName))
            {
                query = query.Where(c => c.LastName.Equals(searchCriteria.LastName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchCriteria.Email))
            {
                query = query.Where(c => c.Email.Equals(searchCriteria.Email, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();

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