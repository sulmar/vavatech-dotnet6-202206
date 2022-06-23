
using Bogus;
using Grpc.Net.Client;
using ProtoBuf;
using ProtoBuf.Grpc.Client;
using Vavatech.Shopper.Contracts;

Console.WriteLine("Hello, gRPC Code First Client!");

// Install-Package Grpc.Net.Client
// Install-Package protobuf-net.Grpc

const string url = "https://localhost:7157";

string proto = Serializer.GetProto<IShopperDeliveryService>();

Console.WriteLine(proto);

var channel = GrpcChannel.ForAddress(url);

// var client = new DeliveryService.DeliveryServiceClient(channel);

// Utworzenie klienta na podstawie kontraktu
var client = channel.CreateGrpcService<IShopperDeliveryService>();

var requests = new Faker<ConfirmDeliveryRequest>()
    .RuleFor(p => p.DeliveryId, f => f.Random.Int(1))
    .RuleFor(p => p.IsShipped, f => f.Random.Bool(0.9f))
    .GenerateLazy(10);

foreach (var request in requests)
{
    Console.WriteLine($"Sending {request.DeliveryId} {request.IsShipped}");
    var response = await client.ConfirmDeliveryAsync(request);
    Console.WriteLine($"Sent. {response.IsConfirmed}");

    await Task.Delay(TimeSpan.FromSeconds(1));
}


Console.WriteLine("Press any key to exit.");
Console.ReadKey();
