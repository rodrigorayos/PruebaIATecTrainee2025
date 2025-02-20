using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Common;

namespace Agenda.Domain.Repositories.Agenda;

public interface IEventRepository : IGenericRepository<EventModel>
{
    Task<IEnumerable<EventModel>> GetByAgendaIdAsync(Guid agendaId);
}