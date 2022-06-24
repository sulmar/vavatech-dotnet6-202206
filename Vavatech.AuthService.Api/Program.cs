using Bogus;
using Microsoft.AspNetCore.Identity;
using Vavatech.AuthService.Api.Models;
using Vavatech.AuthService.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();
builder.Services.AddSingleton<Faker<User>, UserFaker>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();

// PasswordHasher
// ASP.NET Core Identity Version 3: PBKDF2 with HMAC-SHA256, 128-bit salt, 256-bit subkey, 10000 iterations
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// POST api/tokens/create

app.MapPost("api/tokens/create", (AuthModel model, IAuthService authService, ITokenService tokenService) =>
{
    if (authService.TryAuthorize(model.Login, model.Password, out User user))
    {
        var token = tokenService.Create(user);

        return Results.Ok(token);
    }

    return Results.BadRequest(new { message = "Username or password is incorrect."});
   
}).AllowAnonymous();

app.Run();
