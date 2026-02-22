using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Application.Users.Commands;

public class DeleteUserCommand
{
    public static async Task<bool> Handle(int id, AppDbContext db)
    {
        var user = await db.Users.FindAsync(id);//busca por id

        if (user is null) return false;

        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return true;
        
    }

}
