namespace Agenda.Domain.Models.Agenda;

public class EventModel : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public List<string> Participants { get; set; } = new();
    
    public EventModel(
        int id,
        string name,
        string description,
        DateTime date,
        string location,
        List<string> participants
    ) : base(id)
    {
        Name = name;
        Description = description;
        Date = date;
        Location = location;
        Participants = participants;
    }
}