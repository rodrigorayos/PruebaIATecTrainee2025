namespace Agenda.Infrastructure.Database.Entities.Common;

public class BaseEntity : IIdentifiable
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; }

    public DateTime? LastModifiedByAt { get; set; } 
    public Guid? LastModifiedBy { get; set; }
}