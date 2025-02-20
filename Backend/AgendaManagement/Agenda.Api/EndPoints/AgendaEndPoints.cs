using Agenda.Application.Services;
using Agenda.Domain.Models.Agenda;

namespace Agenda.Api.Endpoints;

public static class AgendaEndpoints
{
    internal static void MapAgendaEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/agendas").WithTags("Agendas").MapGroupAgendas();
    }

    internal static void MapGroupAgendas(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("", 
            async (AgendaModel agendaModel, AgendaService service) => 
                await service.CreateAsync(agendaModel));

        groupBuilder.MapGet("", 
            (AgendaService service) => service.GetAllAsync());

        groupBuilder.MapGet("/{id:guid}", 
            async (Guid id, AgendaService service) => 
                await service.GetByIdAsync(id));

        groupBuilder.MapPut("/{id:guid}", 
            async (Guid id, AgendaModel updateModel, AgendaService service) => 
                await service.UpdateAsync(updateModel));

        groupBuilder.MapDelete("/{id:guid}", 
            async (Guid id, AgendaService service) => 
                await service.DeleteAsync(id));
    }
}