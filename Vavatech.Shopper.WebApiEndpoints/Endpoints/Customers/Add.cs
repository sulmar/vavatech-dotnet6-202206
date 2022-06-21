using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.WebApiEndpoints.Endpoints.Customers
{
    public class Add : EndpointBaseSync.WithRequest<Customer>.WithActionResult<Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public Add(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpPost("api/customers")]
        public override ActionResult<Customer> Handle(Customer customer)
        {
            customerRepository.Add(customer);

            return CreatedAtRoute("GetCustomerById", new { customer.Id }, customer);
        }
    }
}
