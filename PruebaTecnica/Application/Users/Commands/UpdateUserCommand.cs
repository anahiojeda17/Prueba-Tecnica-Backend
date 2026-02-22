using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Application.Users.Commands;
public record UpdateUserRequest(string Name, string Email, bool IsActive);

public class UpdateUserCommand
{
    public static async Task<bool> Handle(int id,UpdateUserRequest request, AppDbContext db)
    {
        var user = await db.Users.FindAsync(id);//busca por id

        if (user is null) return false;

        user.Name = request.Name;
        user.Email = request.Email;
        user.IsActive = request.IsActive;
        //si retorna true actualiza y guarda los datos
        await db.SaveChangesAsync();
        return true;


    }

}
