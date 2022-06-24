using Bogus;
using Microsoft.Extensions.Options;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.Infrastructure
{
    public class FakeEmployeeRepository : FakeEntityRepository<Employee>, IEmployeeRepository
    {
        public FakeEmployeeRepository(Faker<Employee> faker, IOptions<FakeEntityOptions> options) : base(faker, options)
        {
        }
    }
}