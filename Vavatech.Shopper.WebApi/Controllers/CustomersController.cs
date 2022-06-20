using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.WebApi.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        // GET api/ping
        [HttpGet("/api/ping")]
        public string Ping()
        {
            return "Pong";
        }

        // GET api/customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var customers = _customerRepository.Get();

            return customers;
        }

        // GET api/customers/{id}

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }    

            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public ActionResult<Customer> Add(Customer customer)
        {
            _customerRepository.Add(customer);

            //return Created($"https://localhost:5001/api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        }


        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            _customerRepository.Update(customer);

            return NoContent();

        }

        // PATCH

    }
}
