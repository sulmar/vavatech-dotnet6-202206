using Grpc.Core;

namespace Vavatech.Shopper.gRPCService.Services
{
    public class ShopperDeliveryService : Vavatech.Shopper.gRPCService.DeliveryService.DeliveryServiceBase
    {
        private readonly ILogger<ShopperDeliveryService> logger;

        public ShopperDeliveryService(ILogger<ShopperDeliveryService> logger)
        {
            this.logger = logger;
        }

        public override Task<ConfirmDeliveryResponse> ConfirmDelivery(ConfirmDeliveryRequest request, ServerCallContext context)
        {
            logger.LogInformation("{DeliveryId} {IsShipped}", request.DeliveryId, request.IsShipped);

            var response = new ConfirmDeliveryResponse { IsConfirmed = true };

            return Task.FromResult(response);

        }
    }
}
