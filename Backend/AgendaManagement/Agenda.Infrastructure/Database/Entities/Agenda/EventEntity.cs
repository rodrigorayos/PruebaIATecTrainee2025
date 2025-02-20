using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agenda.Infrastructure.Database.Entities.Common;

namespace Agenda.Infrastructure.Database.Entities.Agenda;

[Table("Events")]
public class EventEntity : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string Participants { get; set; } = string.Empty; // Guardamos como CSV

    // Relación con Agenda
    [Required]
    public Guid AgendaId { get; set; }

    [ForeignKey("AgendaId")]
    public AgendaEntity? Agenda { get; set; }
}