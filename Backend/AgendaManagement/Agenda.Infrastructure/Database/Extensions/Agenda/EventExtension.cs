using Agenda.Domain.Dtos;
using Agenda.Domain.Models.Agenda;
using Agenda.Infrastructure.Database.Entities.Agenda;

namespace Agenda.Infrastructure.Database.Extensions.Agenda;

public static class EventExtension
{
    public static EventEntity ToEntity(this EventModel model)
    {
        return model is null ? null : new EventEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Date = model.Date,
            Location = model.Location,
            Participants = string.Join(";", model.Participants), // Guardamos como CSV
            AgendaId = model.AgendaId
        };
    }

    public static EventEntity ToEntity(this EventDto dto, Guid agendaId)
    {
        return dto is null ? null : new EventEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            Date = dto.Date,
            Location = dto.Location,
            Participants = string.Join(";", dto.Participants),
            AgendaId = agendaId
        };
    }

    public static EventModel ToModel(this EventEntity entity, AgendaModel agenda)
    {
        return entity is null ? null : new EventModel(
            entity.Name,
            entity.Description,
            entity.Date,
            entity.Location,
            entity.Participants?.Split(';').ToList() ?? new List<string>(),
            entity.AgendaId,
            agenda
        );
    }

    public static EventDto ToDto(this EventEntity entity)
    {
        return entity is null ? null : new EventDto(
            entity.Name,
            entity.Description,
            entity.Date,
            entity.Location,
            entity.Participants?.Split(';').ToList() ?? new List<string>()
        );
    }

    public static List<EventDto> ToDtoList(this IEnumerable<EventEntity> entities)
    {
        return entities?.Select(e => e.ToDto()).ToList() ?? new List<EventDto>();
    }

    public static List<EventModel> ToModelList(this IEnumerable<EventEntity> entities, AgendaModel agenda)
    {
        return entities?.Select(e => e.ToModel(agenda)).ToList() ?? new List<EventModel>();
    }
}
