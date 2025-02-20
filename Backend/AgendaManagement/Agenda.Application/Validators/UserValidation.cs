using Agenda.Domain.Models.Agenda;
using FluentValidation;

namespace Agenda.Application.Validators;

public class UserValidation : AbstractValidator<UserModel>
{
    public UserValidation()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

        RuleFor(u => u.Lastname)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("Debe ser un correo electrónico válido.");

        RuleFor(u => u.PasswordHash)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
    }
}