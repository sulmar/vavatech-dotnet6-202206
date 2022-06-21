
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Logger (middleware)
app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");

    await next();

    Console.WriteLine($"{context.Response.StatusCode}");

});

// Authorization (middleware)
app.Use(async (context, next) =>
{
    if (context.Request.Headers.TryGetValue("Authorization", out var value))
    {
        await next();
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
});


app.Use(async (context, next) =>
{
    if (context.Request.Method == "GET" && context.Request.Path=="/api/customers")
    {
        await context.Response.WriteAsync("Hello Customers");
    }
    else
    {
        await next();
    }    
});


app.Run(async context =>
{  
    await context.Response.WriteAsync("Hello World!");
});

app.Run();
