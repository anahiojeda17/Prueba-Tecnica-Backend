using FluentValidation;

namespace PruebaTecnica.Application.Addresses.Commands;
public class UpdateAddressValidator : AbstractValidator<UpdateAddressRequest>
{
    //validaciones, street, city, country obligatorio
    public UpdateAddressValidator()
    {
       RuleFor(x => x.Street)
            .NotEmpty().WithMessage("La calle es requerida")
            .NotEqual("string").WithMessage("Ingrese una calle válida");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerida")
            .NotEqual("string").WithMessage("Ingrese una ciudad válida");//que no tome lo que viene por defecto 

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("El país es requerido")
            .NotEqual("string").WithMessage("Ingrese un país válido");

    }


}