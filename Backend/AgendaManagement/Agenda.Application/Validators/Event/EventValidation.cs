using Agenda.Domain.Models.Agenda;
using FluentValidation;

namespace Agenda.Application.Validators.Event;

public class EventValidation : AbstractValidator<EventModel>
{
    public EventValidation()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("El nombre del evento es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(e => e.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

        RuleFor(e => e.Date)
            .NotEmpty().WithMessage("La fecha del evento es obligatoria.")
            .GreaterThan(DateTime.Now).WithMessage("La fecha debe ser futura.");

        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("El lugar del evento es obligatorio.")
            .MaximumLength(200).WithMessage("El lugar no puede exceder los 200 caracteres.");

        RuleFor(e => e.Participants)
            .NotEmpty().WithMessage("Debe haber al menos un participante.")
            .Must(p => p.Count > 0).WithMessage("Debe haber al menos un participante en el evento.");
    }
}