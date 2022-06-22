using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Formatting.Compact;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Domain.Validators;
using Vavatech.Shopper.Infrastructure;
using Vavatech.Shopper.Infrastructure.Fakers;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;
using Vavatech.Shopper.WebApi.Controllers;
using Vavatech.Shopper.WebApi.Hubs;
using Vavatech.Shopper.WebApi.RouteConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

// Install-Package Serilog.AspNetCore
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File(new CompactJsonFormatter(), "logs/log.json")
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

// Uruchomienie Seq jako kontener w Docker
// docker run --name my-seq -d -e ACCEPT_EULA=y -p 5341:80 datalust/seq
builder.Logging.AddSerilog(logger);

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
builder.Services.AddSingleton<IProductRepository, FakeProductRepository>();
builder.Services.AddSingleton<Faker<Customer>, CustomerFaker>();
builder.Services.AddSingleton<Faker<Product>, ProductFaker>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IMessageService, SignalRMessageService>();

// builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>();


// Install-Package Hangfire.AspNetCore
// Install-Package Hangfire.InMemory
// builder.Services.AddHangfire(options => options.UseInMemoryStorage());


// Install-Paclage Hangfire.SqlServer
builder.Services.AddHangfire(options => options.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

builder.Services.AddSignalR();

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/Assets"
});

app.MapControllers();


app.MapHangfireDashboard();

app.MapHub<CustomersHub>("signalr/customers");

app.Run();
