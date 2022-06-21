using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Converters;
using Vavatech.Shopper.Domain.Validators;
using Vavatech.Shopper.Infrastructure;
using Vavatech.Shopper.Infrastructure.Fakers;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;
using Vavatech.Shopper.WebApi.RouteConstraints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson

// Install-Package FluentValidation.AspNetCore

builder.Services.AddControllers()
    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CustomerValidator>())
    .AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();
builder.Services.AddSingleton<Faker<Customer>, CustomerFaker>();

// builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>();

// Rejestracja w³asnej regu³y
builder.Services.Configure<RouteOptions>(options => options.ConstraintMap.Add("barcode", typeof(BarcodeRouteConstraint)));

builder.Services.Configure<FakeEntityOptions>(builder.Configuration.GetSection("FakeOptions"));

string environmentName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile("appsettings.json", false);
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", false);
builder.Configuration.AddXmlFile("appsettings.xml", true);
builder.Configuration.AddIniFile("appsettings.ini", true);
// builder.Configuration.AddCommandLine(args);
// builder.Configuration.AddEnvironmentVariables();
// builder.Configuration.AddInMemoryCollection();

string connectionString = builder.Configuration.GetConnectionString("ShopperConnection");

// builder.Services.AddDbContext()


var app = builder.Build();

string nbpUri = builder.Configuration["NBP:Uri:Host"];
string nbpPort = builder.Configuration["NBP:Uri:Port"] ?? "80";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
