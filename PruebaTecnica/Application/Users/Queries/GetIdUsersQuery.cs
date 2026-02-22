using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Users.Queries;
public class GetIdUsersQuery
{
    public static async Task<User?> Handle(int id, AppDbContext db)
    {
        return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

}
