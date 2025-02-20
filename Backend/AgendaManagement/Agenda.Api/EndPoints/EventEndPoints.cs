using Agenda.Application.Services;
using Agenda.Domain.Dtos;
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

        groupBuilder.MapGet("/{id:int}", 
            async (int id, EventService service) => 
                await service.GetByIdAsync(id));

        groupBuilder.MapGet("/date/{date:datetime}", 
            async (DateTime date, EventService service) => 
                await service.GetByDateAsync(date));

        groupBuilder.MapPut("/{id:int}", 
            async (int id, EventModel updateModel, EventService service) => 
                await service.UpdateAsync(updateModel));

        groupBuilder.MapDelete("/{id:int}", 
            async (int id, EventService service) => 
                await service.DeleteAsync(id));
    }
}