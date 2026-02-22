using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net; //hash 


namespace PruebaTecnica.Application.Users.Commands;

public record CreateUserRequest(string Name, string Email, string Password);

public class CreateUserCommand
{
    public static async Task<IResult> Handle(CreateUserRequest request, AppDbContext db)
    {
        // Validacion si el email ya existe
        var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return Results.Conflict("El email ya está registrado");

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = true
        };
        
        //si pasa todo, agrega el nuevo usuario a la db
        db.Users.Add(user);
        await db.SaveChangesAsync();

        //devuelve el usuario 
        return Results.Created($"/users/{user.Id}", user);
    }
    
}
