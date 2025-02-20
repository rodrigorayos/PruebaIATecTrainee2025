using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Common;

namespace Agenda.Domain.Repositories.Agenda;

public interface IEventRepository : IGenericRepository<EventModel>
{
    Task<List<EventModel>> GetByDateAsync(DateTime date);
}