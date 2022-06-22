using Microsoft.AspNetCore.SignalR;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.SignalRServer.Hubs
{
    public class CustomersStrongTypedHub : Hub<ICustomerClient>, ICustomerHub
    {
        private readonly ILogger<CustomersHub> logger;

        public CustomersStrongTypedHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            // zła praktyka
            // logger.LogInformation($"Connected {Context.ConnectionId}");

            // dobra praktyka
            logger.LogInformation("Connected {ConnectionId}", Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task SendAddedCustomer(Customer customer)
        {
            await this.Clients.Others.NewCustomer(customer);
        }

        public async Task Ping()
        {
            await this.Clients.Caller.Pong();
        }
    }
}
