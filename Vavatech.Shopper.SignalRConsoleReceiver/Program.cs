using Microsoft.AspNetCore.SignalR.Client;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Models;

Console.BackgroundColor = ConsoleColor.DarkGreen;
Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine("Hello, Signal-R Receiver!");

const string url = "https://localhost:7180/signalr/customers";

// Install-Package Microsoft.AspNetCore.SignalR.Client

HubConnection connection = new HubConnectionBuilder()
    .WithUrl(url)
    .Build();

connection.On<Customer>(nameof(ICustomerClient.NewCustomer),
    customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

// connection.OnNewCustomer(customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

Console.WriteLine($"Connecting... {url}");
await connection.StartAsync();
Console.WriteLine($"Connected. {connection.ConnectionId}");



Console.WriteLine("Press any key to exit.");
Console.ReadKey();

await connection.StopAsync();

Console.ResetColor();