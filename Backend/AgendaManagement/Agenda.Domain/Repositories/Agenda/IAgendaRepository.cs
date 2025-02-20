using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Common;

namespace Agenda.Domain.Repositories.Agenda;

public interface IAgendaRepository : IGenericRepository<AgendaModel>
{
    Task<IEnumerable<AgendaModel>> GetByUserIdAsync(Guid userId);
}