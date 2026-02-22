using FluentValidation;

namespace PruebaTecnica.Application.Users.Commands;
public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email no debe estar vacio")
            .EmailAddress().WithMessage("El email no tiene un formato válido");

    }

}