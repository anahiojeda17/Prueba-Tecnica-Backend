using FluentValidation;

namespace PruebaTecnica.Application.Currencies.Commands;
public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyRequest>
{
    public CreateCurrencyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El Code es requerido")
            .NotEqual("string").WithMessage("Ingrese un Code válido");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .NotEqual("string").WithMessage("Ingrese un Nombre válido");

        RuleFor(x => x.RateToBase)
            .GreaterThan(0).WithMessage("La tasa debe ser mayor a 0");
            
    }

}