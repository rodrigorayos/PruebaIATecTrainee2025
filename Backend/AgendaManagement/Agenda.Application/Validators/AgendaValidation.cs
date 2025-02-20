using Agenda.Domain.Models.Agenda;
using FluentValidation;

namespace Agenda.Application.Validators;

public class AgendaValidation : AbstractValidator<AgendaModel>
{
    public AgendaValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("El nombre de la agenda es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(a => a.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
    }
}