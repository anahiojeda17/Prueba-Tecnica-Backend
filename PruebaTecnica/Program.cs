using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Infrastructure;
using PruebaTecnica.Application.Users.Commands;
using PruebaTecnica.Application.Users.Queries;
using PruebaTecnica.Application.Addresses.Commands;
using PruebaTecnica.Application.Addresses.Queries;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// definicion de servicios 
builder.Services.AddEndpointsApiExplorer();
//swagger para ingresar el api key 
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
//listar users por estado
app.MapGet("/users", async (bool? isActive, AppDbContext db) =>
{
    var users = await GetAllUsersQuery.Handle(isActive, db);
    return Results.Ok(users);

});
//listar por id
app.MapGet("/users {id}", async (int id, AppDbContext db) =>
{
    var user = await GetIdUsersQuery.Handle(id, db);
    if (user is null)
        return Results.NotFound("Usuario no encontrado");
    
    return Results.Ok(user);
});

//modificar users 
app.MapPut("/users {id}", async(int id, UpdateUserRequest request, AppDbContext db) =>
{
    var validator = new UpdateUserValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    var updated = await UpdateUserCommand.Handle(id, request, db);
    
    if (!updated)
        return Results.NotFound("Usuario no encontrado");

    return Results.Ok("Usuario actualizado");   
});

//eliminar users 
app.MapDelete("/users {id}", async(int id, AppDbContext db) =>
{
    var delete = await DeleteUserCommand.Handle(id, db);
    
    if(!delete)
        return Results.NotFound("Usuario no encontrado");

    return Results.Ok("Usuario Eliminado");
});

//endpoints de Addreses
//crear la direccion de dicho usuario 
app.MapPost("/users/{userId}/addresses", async (int userId, CreateAddressRequest request, AppDbContext db) =>
{
    var validator = new CreateAddressValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    var created = await CreateAddressCommand.Handle(userId, request, db);

    if (!created)
        return Results.NotFound("Usuario no encontrado");

    return Results.Created($"/users/{userId}/addresses", request);
});

//listar las direcciones de un usuario 
app.MapGet("/users/{userId}/addresses", async (int userId, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(userId);
    if (user is null)
        return Results.NotFound("Usuario no encontrado");

    var addresses = await GetAllAddressUserQuery.Handle(userId, db);
    return Results.Ok(addresses);
});

//modificar direcciones 
app.MapPut("/addresses/{id}", async (int id, UpdateAddressRequest request, AppDbContext db) =>
{
    var validator = new UpdateAddressValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    var updated = await UpdateAddressCommand.Handle(id, request, db);

    if (!updated)
        return Results.NotFound("Dirección no encontrada");

    return Results.Ok("Dirección actualizada");
});
//eliminar direccion
app.MapDelete("/addresses/{id}", async (int id, AppDbContext db) =>
{
    var deleted = await DeleteAddressCommand.Handle(id, db);

    if (!deleted)
        return Results.NotFound("Dirección no encontrada");

    return Results.Ok("Dirección eliminada");
});


app.Run();