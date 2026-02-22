using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Addresses.Queries;

public class GetAllAddressUserQuery
{
    public static async Task<IResult> Handle(int userId, AppDbContext db)
    {
        var addresses = await db.Addresses
            .Where(a => a.UserId == userId)
            .Select(a => new {
                a.Id,
                a.UserId,
                userName = a.User.Name,
                a.Street,
                a.City,
                a.Country,
                a.ZipCode
            })
            .ToListAsync();

        return Results.Ok(addresses);
    }
}