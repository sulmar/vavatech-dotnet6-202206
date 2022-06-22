using Microsoft.AspNetCore.SignalR;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Models;

namespace Vavatech.Shopper.WebApi.Hubs
{
    public class CustomersHub : Hub<ICustomerClient>, ICustomerHub
    {
        private readonly ILogger<CustomersHub> logger;

        public CustomersHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            // zła praktyka
            // logger.LogInformation($"Connected {Context.ConnectionId}");

            // dobra praktyka
            logger.LogInformation("Connected {ConnectionId}", Context.ConnectionId);

            // this.Context.User
            // await this.Groups.AddToGroupAsync(Context.ConnectionId, "GroupA");

            await base.OnConnectedAsync();
        }

        //public async Task Join(string groupName)
        //{
        //    await this.Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //}

        public async Task SendAddedCustomer(Customer customer)
        {
            await this.Clients.Others.NewCustomer(customer);

            // await this.Clients.Group("GroupA").NewCustomer(customer);
        }

        public async Task Ping()
        {
            await this.Clients.Caller.Pong();
        }
    }
}
