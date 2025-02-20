using Agenda.Domain.Dtos;
using Agenda.Domain.Models.Agenda;
using Agenda.Infrastructure.Database.Entities.Agenda;

namespace Agenda.Infrastructure.Database.Extensions.Agenda;

public static class EventExtension
{
    public static EventEntity ToEntity(this EventModel model)
    {
        return model == null ? null : new EventEntity
        {
            Name = model.Name,
            Description = model.Description,
            Date = model.Date,
            Location = model.Location,
            Participants = string.Join(",", model.Participants) // Guardamos como CSV
        };
    }

    public static EventEntity ToEntity(this EventDto dto)
    {
        return dto == null ? null : new EventEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            Date = dto.Date,
            Location = dto.Location,
            Participants = string.Join(",", dto.Participants)
        };
    }

    public static EventModel ToModel(this EventEntity entity)
    {
        return entity == null ? null : new EventModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Date,
            entity.Location,
            entity.Participants?.Split(',').ToList() ?? new List<string>() // Convertimos CSV a lista
        );
    }

    public static EventDto ToDto(this EventEntity entity)
    {
        return entity == null ? null : new EventDto(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Date,
            entity.Location,
            entity.Participants?.Split(',').ToList() ?? new List<string>()
        );
    }

    public static List<EventDto> ToDtoList(this IEnumerable<EventEntity> entities)
    {
        return entities?.Select(e => e.ToDto()).ToList() ?? new List<EventDto>();
    }

    public static List<EventModel> ToModelList(this IEnumerable<EventEntity> entities)
    {
        return entities?.Select(e => e.ToModel()).ToList() ?? new List<EventModel>();
    }
}
