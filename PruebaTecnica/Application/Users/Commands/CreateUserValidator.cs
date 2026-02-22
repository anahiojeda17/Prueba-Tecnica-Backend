using FluentValidation;

namespace PruebaTecnica.Application.Users.Commands;
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    //validaciones, name no vacio, email no vacio y debe tener formato valido 
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .NotEqual("string").WithMessage("Ingrese un Nombre válido");//que no tome lo que viene por defecto 


        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email no debe estar vacio")
            .EmailAddress().WithMessage("El email debe tener un formato valido")
            .NotEqual("string").WithMessage("Ingrese un Email válido");//que no tome lo que viene por defecto 


    }


}