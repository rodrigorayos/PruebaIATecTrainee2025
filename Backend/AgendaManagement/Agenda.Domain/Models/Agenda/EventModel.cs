using Agenda.Domain.Models.Common;

namespace Agenda.Domain.Models.Agenda;

public class EventModel : BaseModel
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime Date { get; private set; }
    public string Location { get; private set; }
    public List<string> Participants { get; private set; }
    public Guid AgendaId { get; private set; }
    public AgendaModel Agenda { get; private set; }

    public EventModel(string name, string description, DateTime date, string location, List<string> participants, Guid agendaId, AgendaModel agenda)
    {
        Name = name;
        Description = description;
        Date = date;
        Location = location;
        Participants = participants ?? new List<string>();
        AgendaId = agendaId;
        Agenda = agenda;
    }
}