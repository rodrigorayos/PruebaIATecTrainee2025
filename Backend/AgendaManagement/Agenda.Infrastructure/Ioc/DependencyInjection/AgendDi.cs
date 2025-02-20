using Agenda.Application.Services;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Infrastructure.Database.Repositories.Agenda;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Infrastructure.Ioc.DependencyInjection;

public static class AgendDi
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<EventService>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IEventRepository, EventRepository>();
        return services;
    }
}