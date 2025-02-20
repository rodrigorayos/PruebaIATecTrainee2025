using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agenda.Infrastructure.Database.Entities.Common;

namespace Agenda.Infrastructure.Database.Entities.Agenda;

[Table("Event", Schema = "AGD")]
public class EventEntity : BaseEntity, IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [Column("date")]
    public DateTime Date { get; set; }

    [Required]
    [Column("location")]
    [StringLength(200)]
    public string Location { get; set; }

    [Column("participants")]
    public string Participants { get; set; } = string.Empty;
}