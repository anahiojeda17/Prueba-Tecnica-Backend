using FluentValidation;

namespace PruebaTecnica.Application.CurrencyConversion;

public class ConvertCurrencyValidator : AbstractValidator<ConvertCurrencyRequest>
{
    public ConvertCurrencyValidator()
    {
        RuleFor(x => x.FromCurrencyCode)
            .NotEmpty().WithMessage("El código de moneda origen es requerido")
            .NotEqual("string").WithMessage("Ingrese un Codigo de moneda origen válido");


        RuleFor(x => x.ToCurrencyCode)
            .NotEmpty().WithMessage("El código de moneda destino es requerido")
            .NotEqual("string").WithMessage("Ingrese un Codigo de moneda destino válido");


        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0");
    }
}