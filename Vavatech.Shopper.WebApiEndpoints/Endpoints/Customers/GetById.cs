using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.WebApiEndpoints.Endpoints.Customers
{
    // GET api/customers/{id}

    public class GetById : EndpointBaseSync.WithRequest<int>.WithActionResult<Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public GetById(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("api/customers/{id}", Name = "GetCustomerById")]
        public override ActionResult<Customer> Handle(int id)
        {
            var customer = customerRepository.Get(id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
