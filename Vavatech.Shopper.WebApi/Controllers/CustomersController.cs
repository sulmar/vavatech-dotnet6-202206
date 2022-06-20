using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.WebApi.Controllers
{
    public class CustomersController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        // GET api/ping
        [HttpGet("api/ping")]
        public string Ping()
        {
            return "Pong";
        }

        // GET api/customers
        [HttpGet("api/customers")]
        public IEnumerable<Customer> Get()
        {
            var customers = _customerRepository.Get();

            return customers;
        }

        // GET api/customers/{id}

        [HttpGet("api/customers/{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return new NotFoundResult();
            }    

            return new OkObjectResult(customer);
        }
    }
}
