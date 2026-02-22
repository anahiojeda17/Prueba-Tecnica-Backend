using FluentValidation;

namespace PruebaTecnica.Application.Users.Commands;
public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .NotEqual("string").WithMessage("Ingrese una Nombre válido");//que no tome lo que viene por defecto 


        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email no debe estar vacio")
            .EmailAddress().WithMessage("El email no tiene un formato válido")
            .Matches(@".+\..+").WithMessage("El email debe contener un dominio válido") //agregado por que solo con emailaddress no me detectaba el .com
            .NotEqual("string").WithMessage("Ingrese una Nombre válido");
    }

}