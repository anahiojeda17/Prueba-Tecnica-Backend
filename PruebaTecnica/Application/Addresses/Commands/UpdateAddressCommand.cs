using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Application.Addresses.Commands;
public record UpdateAddressRequest(string Street, string City, string Country, string? ZipCode);

public class UpdateAddressCommand
{
    public static async Task<bool> Handle(int id ,UpdateAddressRequest request, AppDbContext db)
    {
        var address = await db.Addresses.FindAsync(id);//busca por id
        if (address is null) return false;

        address.Street = request.Street;
        address.City = request.City;
        address.Country = request.Country;
        address.ZipCode = request.ZipCode;
       
        //si retorna true actualiza y guarda los datos
        await db.SaveChangesAsync();
        return true;


    }

}
