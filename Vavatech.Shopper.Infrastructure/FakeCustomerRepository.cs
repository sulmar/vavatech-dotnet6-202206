using Bogus;
using Microsoft.Extensions.Options;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;
using Vavatech.Shopper.Models.SearchCriterias;

namespace Vavatech.Shopper.Infrastructure
{



    public class FakeProductRepository : FakeEntityRepository<Product>, IProductRepository
    {
        public FakeProductRepository(Faker<Product> faker, IOptions<FakeEntityOptions> options) : base(faker, options)
        {
        }

        public IEnumerable<Product> Get(ProductSearchCriteria searchCriteria)
        {
            var query = entities.Values.AsQueryable();

            if (searchCriteria.FromPrice.HasValue)
            {
                query = query.Where(p => p.UnitPrice >= searchCriteria.FromPrice);
            }

            if (searchCriteria.ToPrice.HasValue)
            {
                query = query.Where(p => p.UnitPrice <= searchCriteria.FromPrice);
            }

            if (!string.IsNullOrEmpty(searchCriteria.Color))
            {
                query = query.Where(p => p.Color.Equals(searchCriteria.Color, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }
    }

    public class FakeCustomerRepository : FakeEntityRepository<Customer>, ICustomerRepository
    {
        private IDictionary<int, Customer> customers;

        public FakeCustomerRepository(Faker<Customer> faker, IOptions<FakeEntityOptions> options) : base(faker, options)
        {
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

        public Customer GetByEmail(string email)
        {
            return entities.Values.SingleOrDefault(c => c.Email.Equals(email));
        }

        public override void Remove(int id)
        {
            Customer customer = Get(id);
            customer.IsRemoved = true;
        }

        public void Update(Customer customer)
        {
            customers[customer.Id] = customer;
            //Remove(customer.Id);
            //customers.Add(customer.Id, customer);
        }
    }
}