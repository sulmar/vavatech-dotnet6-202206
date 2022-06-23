using Bogus;
using Grpc.Net.Client;
using Vavatech.Shopper.gRPCService;

Console.WriteLine("Hello, gRPC Client!");

const string url = " https://localhost:7093";

var channel = GrpcChannel.ForAddress(url);

var client = new DeliveryService.DeliveryServiceClient(channel);

// var request = new ConfirmDeliveryRequest { DeliveryId = 1, IsShipped = true };

var requests = new Faker<ConfirmDeliveryRequest>()
    .RuleFor(p => p.DeliveryId, f => f.Random.Int(1))
    .RuleFor(p=>p.IsShipped, f=>f.Random.Bool(0.9f))
    .GenerateForever();

foreach (var request in requests)
{
    Console.WriteLine($"Sending {request.DeliveryId} {request.IsShipped}");
    var response = await client.ConfirmDeliveryAsync(request);
    Console.WriteLine($"Sent. {response.IsConfirmed}");

  await Task.Delay(TimeSpan.FromSeconds(1));
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();





