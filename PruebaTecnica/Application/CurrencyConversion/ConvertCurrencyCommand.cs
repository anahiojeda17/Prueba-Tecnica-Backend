using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.CurrencyConversion;

public record ConvertCurrencyRequest(string FromCurrencyCode, string ToCurrencyCode, decimal Amount);

public class ConvertCurrencyCommand
{
    public static async Task<IResult> Handle(ConvertCurrencyRequest request, AppDbContext db)
    {
        //toma la primera moneda 
        var from = await db.Currencies.FirstOrDefaultAsync(c => c.Code == request.FromCurrencyCode.ToUpper());
        if (from == null)
            return Results.NotFound($"Moneda '{request.FromCurrencyCode}' no encontrada");
        //toma la segunda moneda 
        var to = await db.Currencies.FirstOrDefaultAsync(c => c.Code == request.ToCurrencyCode.ToUpper());
        if (to == null)
            return Results.NotFound($"Moneda '{request.ToCurrencyCode}' no encontrada");

        //hace la conversion de divisas 
        var montoBase = request.Amount * from.RateToBase;
        var convertedAmount = montoBase / to.RateToBase;

        //devuelve en un json la conversion
        return Results.Ok(new
        {
            fromCurrency = from.Code,
            toCurrency = to.Code,
            originalAmount = request.Amount,
            convertedAmount = Math.Round(convertedAmount, 2)
        });
    }
}