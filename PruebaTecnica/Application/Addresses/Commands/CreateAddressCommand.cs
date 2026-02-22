using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Application.Addresses.Commands;

public record CreateAddressRequest(string Street, string City, string Country, string? ZipCode);

public class CreateAddressCommand
{
    public static async Task<bool> Handle(int userId, CreateAddressRequest request, AppDbContext db)
    {
        var user = await db.Users.FindAsync(userId);//busca por id
        if (user is null) return false;  //si no encuentra retorna false

       var address = new Address
        {
            UserId = userId,
            Street = request.Street,
            City = request.City,
            Country = request.Country,
            ZipCode = request.ZipCode
        };
                
        db.Addresses.Add(address);
        await db.SaveChangesAsync();
        return true;

    }

}