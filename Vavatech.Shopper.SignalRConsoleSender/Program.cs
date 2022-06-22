﻿
using Bogus;
using Microsoft.AspNetCore.SignalR.Client;
using Vavatech.Shopper.Infrastructure.Fakers;
using Vavatech.Shopper.Models;

Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine("Hello, Signal-R Sender!");

const string url = "https://localhost:7180/signalr/customers";

// Install-Package Microsoft.AspNetCore.SignalR.Client

HubConnection connection = new HubConnectionBuilder()
    .WithUrl(url)
    .Build();

Console.WriteLine($"Connecting... {url}");
await connection.StartAsync();
Console.WriteLine($"Connected. {connection.ConnectionId}");

Faker<Customer> faker = new CustomerFaker();

// Customer customer = faker.Generate();

// yield
var customers = faker.GenerateForever();

//var enumerator = customers.GetEnumerator();

//while (enumerator.MoveNext())
//{
//    Console.WriteLine(enumerator.Current);
//}


foreach (var customer in customers)
{
    Console.WriteLine($"Sending {customer.FirstName} {customer.LastName}...");
    await connection.SendAsync("SendAddedCustomer", customer);
    Console.WriteLine("Sent.");

    await Task.Delay(TimeSpan.FromSeconds(0.01));
}


Console.WriteLine("Press any key to exit.");
Console.ReadKey();

await connection.StopAsync();

Console.ResetColor();
