using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.WebApiEndpoints.Endpoints.Customers
{
    // Install-Package Ardalis.ApiEndpoints

    // GET api/customers
    public class GetCustomers : EndpointBaseSync.WithoutRequest.WithResult<IEnumerable<Customer>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomers(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("api/customers")]
        public override IEnumerable<Customer> Handle()
        {
            return customerRepository.Get();
        }
    }
}
