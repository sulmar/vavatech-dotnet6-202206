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

        public override async Task ShipChangedStatus(ShipChangedStatusRequest request, IServerStreamWriter<ShipChangedStatusResponse> responseStream, ServerCallContext context)
        {

            var statuses = NextShipChangedStatus
                .Where(s=>s.DeliveryId == request.DeliveryId);

            foreach (var status in statuses)
            {
                await responseStream.WriteAsync(status);

                await Task.Delay(TimeSpan.FromSeconds(5));
            }            
        }

        
        // Leniwa kolekcja
        private IEnumerable<ShipChangedStatusResponse> NextShipChangedStatus
        { 
            get
            {
                yield return new ShipChangedStatusResponse { DeliveryId = 1, Status = ShipChangedStatusResponse.Types.Status.Registered};
                yield return new ShipChangedStatusResponse { DeliveryId = 2, Status = ShipChangedStatusResponse.Types.Status.Registered };
                yield return new ShipChangedStatusResponse { DeliveryId = 3, Status = ShipChangedStatusResponse.Types.Status.Registered };

                yield return new ShipChangedStatusResponse { DeliveryId = 1, Status = ShipChangedStatusResponse.Types.Status.Inprogress};
                yield return new ShipChangedStatusResponse { DeliveryId = 1, Status = ShipChangedStatusResponse.Types.Status.Shipped };

                yield return new ShipChangedStatusResponse { DeliveryId = 2, Status = ShipChangedStatusResponse.Types.Status.Inprogress };
                yield return new ShipChangedStatusResponse { DeliveryId = 3, Status = ShipChangedStatusResponse.Types.Status.Inprogress };

                yield return new ShipChangedStatusResponse { DeliveryId = 3, Status = ShipChangedStatusResponse.Types.Status.Inprogress };
                yield return new ShipChangedStatusResponse { DeliveryId = 2, Status = ShipChangedStatusResponse.Types.Status.Shipped };

            }
         }
            
    }
}
