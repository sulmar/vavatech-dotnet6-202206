using Vavatech.Shopper.SignalRServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "Use signal-R on singalr/customers");

app.MapHub<CustomersHub>("signalr/customers");

app.Run();
