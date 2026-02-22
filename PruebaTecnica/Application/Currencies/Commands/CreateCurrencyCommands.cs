using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Domain;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Currencies.Commands;

public record CreateCurrencyRequest(string Code, string Name, decimal RateToBase);

public class CreateCurrencyCommand
{
    public static async Task<IResult> Handle(CreateCurrencyRequest request, AppDbContext db)
    {
        //verifica si ya existe el mismo codigo de moneda
        var existing = await db.Currencies.FirstOrDefaultAsync(c => c.Code == request.Code);
        if (existing != null)
            return Results.Conflict("El codigo de moneda ya existe");

        //si no guarda en la tabla
        var currency = new Currency
        {
            Code = request.Code.ToUpper(),//mayuscula
            Name = request.Name,
            RateToBase = request.RateToBase
        };

        db.Currencies.Add(currency);
        await db.SaveChangesAsync();

        return Results.Created($"/currencies/{currency.Id}", currency);
    }
}