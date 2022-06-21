using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Domain.Services;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;
using Vavatech.Shopper.Models.SearchCriterias;

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
        //[HttpGet]
        //public IEnumerable<Customer> Get()
        //{
        //    var customers = _customerRepository.Get();

        //    return customers;
        //}



        // GET api/customers/{id}
        // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-constraints
        [HttpGet("{id:int:min(1)}", Name = "GetCustomerById")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }    

            return Ok(customer);
        }

        // GET api/customers/{lastName}
        [HttpGet("{lastName:minlength(3)}")]
        public ActionResult<Customer> Get(string lastName)
        {
            var customer = _customerRepository.Get(lastName);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        // GET api/customers?firstName=John&lastName=Smith
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get([FromQuery] CustomerSearchCriteria searchCriteria)
        {
            var req = this.HttpContext.Request;

            var customers = _customerRepository.Get(searchCriteria);

            return Ok(customers);
        }

        // POST api/customers
        [HttpPost]
        public ActionResult<Customer> Add([FromBody] Customer customer)
        {
            _customerRepository.Add(customer);

            //return Created($"https://localhost:5001/api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        }


        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            _customerRepository.Update(customer);

            return NoContent();

        }

        // Install-Package Microsoft.AspNetCore.JsonPatch
        // builder.Services.AddControllers().AddNewtonsoftJson();
        // Content-Type: application/json-patch+json
        // PATCH api/customers/{id}
        // JS: https://www.npmjs.com/package/jsonpatch
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Customer> jsonPatch)
        {
            var customer = _customerRepository.Get(id);

            jsonPatch.ApplyTo(customer);

            return NoContent();
        }

        // JSON Merge Patch
        // Content-Type: application/merge-patch+json
        // https://datatracker.ietf.org/doc/html/rfc7386


        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _customerRepository.Remove(id);

            return Ok();
        }


        public ActionResult<decimal> Calculate([FromServices] IPriceCalculatorService priceCalculatorService, int productId, int customerId)
        {
            var price = priceCalculatorService.CalculatePrice(productId, customerId);

            return Ok(price);
        }

       

    }
}
