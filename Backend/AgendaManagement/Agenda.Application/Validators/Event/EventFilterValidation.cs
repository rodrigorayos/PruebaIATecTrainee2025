using FluentValidation;

namespace Agenda.Application.Validators.Event;

public class EventFilterValidation : AbstractValidator<DateTime>
{
    public EventFilterValidation()
    {
        RuleFor(date => date)
            .NotEmpty().WithMessage("La fecha de filtro es obligatoria.")
            .LessThanOrEqualTo(DateTime.Now.AddYears(5)).WithMessage("No se pueden buscar eventos con más de 5 años de anticipación.");
    }
}