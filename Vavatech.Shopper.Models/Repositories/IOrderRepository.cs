namespace Vavatech.Shopper.Models.Repositories;

public interface IOrderRepository : IEntityRepository<Order>
{
    IEnumerable<Order> Get(int customerId);
}
