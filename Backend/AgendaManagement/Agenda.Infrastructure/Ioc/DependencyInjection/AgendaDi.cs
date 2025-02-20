using Agenda.Application.Services;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Infrastructure.Database.Repositories.Agenda;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Infrastructure.Ioc.DependencyInjection;

public static class AgendaDi
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<UserService>();
        services.AddTransient<AgendaService>();
        services.AddTransient<EventService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAgendaRepository, AgendaRepository>();
        services.AddTransient<IEventRepository, EventRepository>();

        return services;
    }
}