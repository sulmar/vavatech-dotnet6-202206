using Bogus;
using Vavatech.Shopper.Infrastructure;
using Vavatech.Shopper.Infrastructure.Fakers;
using Vavatech.Shopper.MinimalApi;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
builder.Services.AddSingleton<Faker<Customer>, CustomerFaker>();

builder.Services.Configure<FakeEntityOptions>(builder.Configuration.GetSection("FakeOptions"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Install-Package Swashbuckle.AspNetCore

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/ping", () => "Pong");

app.MapGet("/api/customers", (ICustomerRepository repository) => Results.Ok(repository.Get()))
    .RequireAuthorization();

app.MapGet("api/test", () => Results.Extensions.NotModified());

//app.MapGet("/api/customers/{id:int:min(1)}", (ICustomerRepository repository, int id) =>
//{
//    var customer = repository.Get(id);

//    if (customer is null)
//    {
//        return Results.NotFound();
//    }

//    return Results.Ok(customer);
//});


// Przyk³ad z u¿yciem operatora is
//app.MapGet("/api/customers/{id:int:min(1)}", (ICustomerRepository repository, int id) => repository.Get(id) is Customer customer 
//? Results.Ok(customer) 
//: Results.NotFound());


// Przyk³ad z u¿yciem operatora switch
app.MapGet("/api/customers/{id:int:min(1)}", (ICustomerRepository repository, int id) => repository.Get(id) switch
{
    Customer customer => Results.Ok(customer),
    _ => Results.NotFound()
})
    .Produces<Customer>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetCustomerById");

app.MapPost("/api/customers", (ICustomerRepository repository, Customer customer) =>
{
    repository.Add(customer);

    return Results.CreatedAtRoute("GetCustomerById", new { Id = customer.Id }, customer);
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
