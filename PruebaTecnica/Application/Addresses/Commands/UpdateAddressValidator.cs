using FluentValidation;

namespace PruebaTecnica.Application.Addresses.Commands;
public class UpdateAddressValidator : AbstractValidator<UpdateAddressRequest>
{
    //validaciones, street, city, country obligatorio
    public UpdateAddressValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street es requerido");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerido");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country es requerido");

    }


}