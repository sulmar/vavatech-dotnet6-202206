using ProtoBuf.Grpc.Server;
using Vavatech.Shopper.gRPCCodeFirstService.Services;

var builder = WebApplication.CreateBuilder(args);

// Install-Package protobuf-net.Grpc.AspNetCore

builder.Services.AddCodeFirstGrpc();

// Install-Package protobuf-net.Grpc.AspNetCore.Reflection
// Install-Package System.ServiceModel.Primitives
builder.Services.AddCodeFirstGrpcReflection();

var app = builder.Build();

app.MapGet("/", () => "Hello gRPC Code First Server!");

app.MapGrpcService<ShopperDeliveryService>();
app.MapCodeFirstGrpcReflectionService();

// dotnet tool install --global dotnet-grpc-cli --version 0.6.0
// dotnet grpc-cli ls https://localhost:7157/
// dotnet grpc-cli ls https://localhost:7157/ Vavatech.Shopper.Contracts.ShopperDeliveryService

// Pobranie pliku proto
// dotnet grpc-cli dump https://localhost:7157/ Vavatech.Shopper.Contracts.ShopperDeliveryService

app.Run();
