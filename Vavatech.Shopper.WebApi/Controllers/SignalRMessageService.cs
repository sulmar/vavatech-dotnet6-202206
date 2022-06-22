using Microsoft.AspNetCore.SignalR;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.WebApi.Hubs;

namespace Vavatech.Shopper.WebApi.Controllers
{
    public class SignalRMessageService : IMessageService
    {
        private readonly IHubContext<CustomersHub> customersHub;

        public SignalRMessageService(IHubContext<CustomersHub> customersHub)
        {
            this.customersHub = customersHub;
        }
        public void Send(string message, Customer customer)
        {
            customersHub.Clients.All.SendAsync(message, customer);
        }
    }
}
