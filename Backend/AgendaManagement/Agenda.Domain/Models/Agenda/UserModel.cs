using Agenda.Domain.Models.Common;

namespace Agenda.Domain.Models.Agenda;

public class UserModel : BaseModel
{
    public string Name { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public List<AgendaModel> Agendas { get; private set; }

    public UserModel(string name, string lastname, string email, string passwordHash)
    {
        Name = name;
        Lastname = lastname;
        Email = email;
        PasswordHash = passwordHash;
        Agendas = new List<AgendaModel>();
    }
}