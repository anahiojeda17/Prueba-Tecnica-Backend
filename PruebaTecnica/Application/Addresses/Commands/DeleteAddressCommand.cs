using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Application.Addresses.Commands;

public class DeleteAddressCommand
{
    public static async Task<bool> Handle(int id, AppDbContext db)
    {
        var address = await db.Addresses.FindAsync(id);//busca por id

        if (address is null) return false;

        db.Addresses.Remove(address);
        await db.SaveChangesAsync();
        return true;
        
    }

}
