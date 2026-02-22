using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Users.Queries;
public class GetAllUsersQuery
{
    public static async Task<List<User>> Handle(bool? isActive, AppDbContext db)
    {
        var query = db.Users.AsQueryable();
        //si trae algun valor lo filtra si no lista completa 
        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);

        return await query.ToListAsync();
    }
}