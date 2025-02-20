namespace Agenda.Domain.Dtos;

public record EventDto(
    int Id,
    string Name,
    string Description,
    DateTime Date,
    string Location,
    List<string> Participants
);

public record EventQueryDto(
    string Name,
    string Description,
    DateTime Date,
    string Location,
    List<string> Participants
);