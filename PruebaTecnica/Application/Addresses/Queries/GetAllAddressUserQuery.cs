using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Addresses.Queries;
public class GetAllAddressUserQuery
{
    public static async Task<List<Address>> Handle(int UserId, AppDbContext db)
    {
       return await db.Addresses
            .Where(a => a.UserId == UserId)
            .ToListAsync();
    }
}