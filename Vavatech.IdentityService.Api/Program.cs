using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vavatech.IdentityService.Api;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("IdentityConnection");

// Install-Package Microsoft.EntityFrameworkCore.SqlServer
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Install-Package Microsoft.AspNetCore.Identity
// Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Install-Package Microsoft.EntityFrameworkCore.tools

// PMC> add-migration InitialMigration
// PMC> update-database

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

// POST /api/users
app.MapPost("/api/users", async (ApplicationUser user, UserManager<ApplicationUser> userManager) =>
{
    var result = await userManager.CreateAsync(user);

    if (result.Succeeded)
        return Results.Ok();
    else
        return Results.BadRequest(result.Errors);

});

app.Run();
