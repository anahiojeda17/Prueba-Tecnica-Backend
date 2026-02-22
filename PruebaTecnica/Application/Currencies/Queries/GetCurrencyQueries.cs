using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Currencies.Queries;
public class GetCurrencyQuery
{
    public static async Task<IResult> Handle(AppDbContext db)
    {
        //listar todas las monedas
        var currencies = await db.Currencies.ToListAsync();
        return Results.Ok(currencies);
    }
}