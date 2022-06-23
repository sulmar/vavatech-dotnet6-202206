using ProtoBuf;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Vavatech.Shopper.Contracts
{
    // Install-Package protobuf-net.Grpc

    [ServiceContract]
    public interface IShopperDeliveryService
    {
        Task<ConfirmDeliveryResponse> ConfirmDeliveryAsync(ConfirmDeliveryRequest request, CallContext context = default);
    }

    [ProtoContract]
    public class ConfirmDeliveryRequest
    {
        [ProtoMember(1)]
        public int DeliveryId { get; set; }
        [ProtoMember(2)]
        public bool IsShipped { get; set; }
    }

    [ProtoContract]
    public class ConfirmDeliveryResponse
    {
        [ProtoMember(1)]
        public bool IsConfirmed { get; set; }
    }
}