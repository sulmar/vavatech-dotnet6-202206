using ProtoBuf.Grpc;
using Vavatech.Shopper.Contracts;

namespace Vavatech.Shopper.gRPCCodeFirstService.Services
{
    public class ShopperDeliveryService : IShopperDeliveryService
    {
        private readonly ILogger<ShopperDeliveryService> logger;

        public ShopperDeliveryService(ILogger<ShopperDeliveryService> logger)
        {
            this.logger = logger;
        }

        public Task<ConfirmDeliveryResponse> ConfirmDeliveryAsync(ConfirmDeliveryRequest request, CallContext context = default)
        {
            logger.LogInformation("{DeliveryId} {IsShipped}", request.DeliveryId, request.IsShipped);

            var response = new ConfirmDeliveryResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }
    }
}
