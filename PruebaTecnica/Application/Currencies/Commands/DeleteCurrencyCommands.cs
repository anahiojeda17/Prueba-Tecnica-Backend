using PruebaTecnica.Infrastructure;

namespace PruebaTecnica.Application.Currencies.Commands;

public class DeleteCurrencyCommand
{
    public static async Task<IResult> Handle(int id, AppDbContext db)
    {
        var currency = await db.Currencies.FindAsync(id);
        if (currency == null)
            return Results.NotFound("Moneda no encontrada");

        db.Currencies.Remove(currency);
        await db.SaveChangesAsync();

        return Results.Ok("Moneda eliminada correctamente");
    }
}