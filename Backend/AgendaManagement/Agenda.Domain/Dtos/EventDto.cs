namespace Agenda.Domain.Dtos;

public record EventDto(string Name, string Description, DateTime Date, string Location, List<string> Participants);
