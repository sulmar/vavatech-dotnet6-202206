using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.WebApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        // GET api/customers/{id}/orders
        [HttpGet("/api/customers/{id}/orders")]
        public ActionResult<IEnumerable<Order>> GetOrders(int customerId)
        {
            var orders = _orderRepository.Get(customerId);

            return Ok(orders);
        }
    }
}
