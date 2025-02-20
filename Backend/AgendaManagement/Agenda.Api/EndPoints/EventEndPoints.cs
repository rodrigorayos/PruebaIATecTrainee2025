using Agenda.Application.Services;
using Agenda.Domain.Models.Agenda;

namespace Agenda.Api.Endpoints;

public static class EventEndpoints
{
    internal static void MapEventEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/events").WithTags("Events").MapGroupEvents();
    }

    internal static void MapGroupEvents(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("", 
            async (EventModel eventModel, EventService service) => 
                await service.CreateAsync(eventModel));

        groupBuilder.MapGet("", 
            (EventService service) => service.GetAllAsync());

        groupBuilder.MapGet("/{id:guid}", 
            async (Guid id, EventService service) => 
                await service.GetByIdAsync(id));

        groupBuilder.MapPut("/{id:guid}", 
            async (Guid id, EventModel updateModel, EventService service) => 
                await service.UpdateAsync(updateModel));

        groupBuilder.MapDelete("/{id:guid}", 
            async (Guid id, EventService service) => 
                await service.DeleteAsync(id));
    }
}