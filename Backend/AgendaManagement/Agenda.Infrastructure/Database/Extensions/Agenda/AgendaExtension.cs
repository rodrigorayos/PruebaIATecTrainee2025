using Agenda.Domain.Dtos;
using Agenda.Domain.Models.Agenda;
using Agenda.Infrastructure.Database.Entities.Agenda;

namespace Agenda.Infrastructure.Database.Extensions.Agenda;

public static class AgendaExtension
{
    public static AgendaEntity ToEntity(this AgendaModel model)
    {
        return model is null ? null : new AgendaEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            UserId = model.UserId
        };
    }

    public static AgendaEntity ToEntity(this AgendaDto dto, Guid userId)
    {
        return dto is null ? null : new AgendaEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };
    }

    public static AgendaModel ToModel(this AgendaEntity entity, UserModel user)
    {
        return entity is null ? null : new AgendaModel(
            entity.Name,
            entity.Description,
            entity.UserId,
            user
        );
    }

    public static AgendaDto ToDto(this AgendaEntity entity)
    {
        return entity is null ? null : new AgendaDto(
            entity.Name,
            entity.Description
        );
    }

    public static List<AgendaDto> ToDtoList(this IEnumerable<AgendaEntity> entities)
    {
        return entities?.Select(e => e.ToDto()).ToList() ?? new List<AgendaDto>();
    }

    public static List<AgendaModel> ToModelList(this IEnumerable<AgendaEntity> entities, UserModel user)
    {
        return entities?.Select(e => e.ToModel(user)).ToList() ?? new List<AgendaModel>();
    }
}