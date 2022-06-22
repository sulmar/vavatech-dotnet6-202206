using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Domain.Services;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;
using Vavatech.Shopper.Models.SearchCriterias;
using Microsoft.AspNetCore.Http;
using QuestPDF.Fluent;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Vavatech.Shopper.WebApi.Hubs;

namespace Vavatech.Shopper.WebApi.Controllers
{

    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        private const int OverSizeLimit = 1_000_000;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        // GET api/ping
        [HttpGet("/api/ping")]
        public string Ping()
        {
            this.HttpContext.Response.WriteAsync("Hello World!");

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
        // Accept: application/json
        // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-constraints
        [HttpGet("{id:int:min(1)}", Name = "GetCustomerById")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Customer>> Add([FromBody] Customer customer, [FromServices] IHubContext<CustomersHub> customersHub)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _customerRepository.Add(customer);

            await customersHub.Clients.All.SendAsync("NewCustomer", customer);

            //return Created($"https://localhost:5001/api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
        }


        // PUT api/customers/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
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


        // GET api/customers/{id}
        // Accept: application/pdf
        [HttpGet("{id}/pdf")]
        public ActionResult GetPdf(int id)
        {
            Customer customer = _customerRepository.Get(id);

            var document = new CustomerDocument(customer);

            Stream stream = new MemoryStream();
            document.GeneratePdf(stream);

            // stream.Position = 0;
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf");
        }

        // GET api/customers/{id}
        // Accept: application/pdf
        [HttpGet("{id}/longpdf")]
        public ActionResult GetLongPdf(int id, [FromServices] IBackgroundJobClient jobClient, [FromServices] IHubContext<CustomersHub> customersHub)
        {
            Customer customer = _customerRepository.Get(id);

            // Uwaga: wersja statyczna, nietestowalna!
            // BackgroundJob.Enqueue(() => GenerateExtensions.GeneratePdf(customer));

            // wersja instacyjna, testowalna :)
            string parentId = jobClient.Enqueue(() => GenerateExtensions.GeneratePdf(customer));

            // Wyślij po zakończeniu poprzedniego zadania
            // jobClient.ContinueJobWith(parentId, () => customersHub.Clients.All.SendAsync("DocumentReady", System.Threading.CancellationToken.None));

            // Wyślij po zakończeniu poprzedniego zadania
            //jobClient.ContinueJobWith<IHubContext<CustomersHub>>(parentId, customersHub => customersHub.Clients.All.SendAsync("DocumentReady", System.Threading.CancellationToken.None));


            //        BackgroundJob.Schedule<IHubContext<MySignalRHub>>(hubContext =>
            //hubContext.Clients.All.SendAsync(
            //    "MyMessage",
            //    "MyMessageContent",
            //    System.Threading.CancellationToken.None),
            //TimeSpan.FromMinutes(2));


            return Accepted();

            // stream.Position = 0;
            //stream.Seek(0, SeekOrigin.Begin);

            //return File(stream, "application/pdf");
        }

        [HttpPost("{id}/photo")]
        public ActionResult Upload(IFormFile formFile, [FromServices] IWebHostEnvironment env)
        {
            if (formFile.ContentType != "image/png")
            {
                return BadRequest("Invalid format");
            }

            if (formFile.Length > OverSizeLimit)
            {
                return BadRequest("Over size limit");
            }

            string path = Path.Combine(env.ContentRootPath, "uploads", formFile.FileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            Stream stream = new FileStream(path, FileMode.Create);
            formFile.CopyTo(stream);

            return Ok();

        }




        //public ActionResult<decimal> Calculate([FromServices] IPriceCalculatorService priceCalculatorService, int productId, int customerId)
        //{
        //    var price = priceCalculatorService.CalculatePrice(productId, customerId);

        //    return Ok(price);
        //}



    }
}
