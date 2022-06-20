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
    }
}
