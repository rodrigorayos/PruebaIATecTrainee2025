using Agenda.Application.Services;
using Agenda.Domain.Models.Agenda;

namespace Agenda.Api.Endpoints;

public static class UserEndpoints
{
    internal static void MapUserEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/users").WithTags("Users").MapGroupUsers();
    }

    internal static void MapGroupUsers(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("/register", 
            async (UserModel userModel, UserService service) => 
                await service.RegisterAsync(userModel));

        groupBuilder.MapGet("", 
            (UserService service) => service.GetAllAsync());

        groupBuilder.MapGet("/{id:guid}", 
            async (Guid id, UserService service) => 
                await service.GetByIdAsync(id));

        groupBuilder.MapPut("/{id:guid}", 
            async (Guid id, UserModel updateModel, UserService service) => 
                await service.UpdateAsync(updateModel));

        groupBuilder.MapDelete("/{id:guid}", 
            async (Guid id, UserService service) => 
                await service.DeleteAsync(id));
    }
}