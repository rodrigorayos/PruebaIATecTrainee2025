namespace Agenda.Infrastructure.Database.Entities.Common;

public interface IIdentifiable
{
    Guid Id { get; set; }
}