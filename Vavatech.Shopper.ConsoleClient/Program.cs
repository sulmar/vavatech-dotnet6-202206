using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Vavatech.Shopper.Infrastructure;
using Vavatech.Shopper.Infrastructure.Fakers;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

Console.WriteLine("Hello, World!");

// Install-Package Microsoft.Extensions.DependencyInjection

// Rejestracja
IServiceCollection services = new ServiceCollection()
    .AddSingleton<ICustomerRepository, FakeCustomerRepository>()
    .AddSingleton<Faker<Customer>, CustomerFaker>();

var serviceProvider = services.BuildServiceProvider();

// Utworzenie instacji bez użycia wstrzykiwania zależności
// ICustomerRepository customerRepository = new FakeCustomerRepository(new CustomerFaker());

// Utworzenie instacji z użyciem wstrzykiwania zależności
ICustomerRepository customerRepository = serviceProvider.GetRequiredService<ICustomerRepository>();

var customers = customerRepository.Get();

foreach (var customer in customers)
{
    Console.WriteLine(customer.FirstName);
}