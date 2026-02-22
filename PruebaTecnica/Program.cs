using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Infrastructure;
using PruebaTecnica.Application.Users.Commands;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// definicion de servicios 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Ingresa tu API Key"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<CreateUserValidator>();


// Base de datos SQLite - que guarde la base de datos en un archivo
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

var app = builder.Build();

// Middleware de API Key
app.Use(async (context, next) =>
{
    // Excluir Swagger del middleware para probar 
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        await next();
        return;
    }

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

//definicion de endpoints users
// crear
app.MapPost("/users", async (CreateUserRequest request, AppDbContext db) =>
{
    var validator = new CreateUserValidator();
    var result = validator.Validate(request);
    //devulve error si es que no pasa la validacion sino, ejecuta el comando
    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));
    return await CreateUserCommand.Handle(request, db);
});


app.Run();