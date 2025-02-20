using Agenda.Domain.Dtos;
using Agenda.Domain.Models.Agenda;
using Agenda.Infrastructure.Database.Entities.Agenda;

namespace Agenda.Infrastructure.Database.Extensions.Agenda;

public static class UserExtension
{
    public static UserEntity ToEntity(this UserModel model)
    {
        return model is null ? null : new UserEntity
        {
            Id = model.Id,
            Name = model.Name,
            LastName = model.Lastname,
            Email = model.Email,
            PasswordHash = model.PasswordHash
        };
    }

    public static UserEntity ToEntity(this UserDto dto)
    {
        return dto is null ? null : new UserEntity
        {
            Name = dto.Name,
            LastName = dto.Lastname,
            Email = dto.Email
        };
    }

    public static UserModel ToModel(this UserEntity entity)
    {
        return entity is null ? null : new UserModel(
            entity.Name,
            entity.LastName,
            entity.Email,
            entity.PasswordHash
        );
    }

    public static UserDto ToDto(this UserEntity entity)
    {
        return entity is null ? null : new UserDto(
            entity.Name,
            entity.LastName,
            entity.Email
        );
    }

    public static List<UserDto> ToDtoList(this IEnumerable<UserEntity> entities)
    {
        return entities?.Select(e => e.ToDto()).ToList() ?? new List<UserDto>();
    }

    public static List<UserModel> ToModelList(this IEnumerable<UserEntity> entities)
    {
        return entities?.Select(e => e.ToModel()).ToList() ?? new List<UserModel>();
    }
}