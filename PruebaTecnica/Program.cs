using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// configuracion para el Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos SQLite - que guarde la base de datos en un archivo
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

var app = builder.Build();

// Middleware de API Key
app.Use(async (context, next) =>
{
    var apiKey = builder.Configuration["ApiKey"];
    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var key) || key != apiKey)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
        return;
    }
    await next();
});

// Swagger UI 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();