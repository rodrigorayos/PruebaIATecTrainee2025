using Agenda.Domain.Models.Common;

namespace Agenda.Domain.Models.Agenda;

public class AgendaModel : BaseModel
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }
    public UserModel User { get; private set; }
    public List<EventModel> Events { get; private set; }

    public AgendaModel(string name, string description, Guid userId, UserModel user)
    {
        Name = name;
        Description = description;
        UserId = userId;
        User = user;
        Events = new List<EventModel>();
    }
}