using FluentValidation;

namespace PruebaTecnica.Application.Addresses.Commands;
public class CreateAddressValidator : AbstractValidator<CreateAddressRequest>
{
    //validaciones, street, city, country obligatorio
    public CreateAddressValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street es requerido");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerido");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country es requerido");

    }


}