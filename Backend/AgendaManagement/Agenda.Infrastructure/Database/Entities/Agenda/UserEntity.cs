using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agenda.Infrastructure.Database.Entities.Common;

namespace Agenda.Infrastructure.Database.Entities.Agenda;

[Table("Users")]
public class UserEntity : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty; // Hash de la contraseña

    // Relación con Agendas
    public ICollection<AgendaEntity>? Agendas { get; set; }
}