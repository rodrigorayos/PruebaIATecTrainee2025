using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agenda.Infrastructure.Database.Entities.Common;

namespace Agenda.Infrastructure.Database.Entities.Agenda;

[Table("Agendas")]
public class AgendaEntity : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    // Relación con User
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity? User { get; set; }

    // Relación con Event
    public ICollection<EventEntity>? Events { get; set; }
}