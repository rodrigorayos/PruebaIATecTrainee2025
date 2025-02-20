using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Infrastructure.Database;
using Agenda.Infrastructure.Database.Entities.Agenda;
using Agenda.Infrastructure.Database.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Infrastructure.Database.Context;
using Agenda.Infrastructure.Database.Extensions.Agenda;

namespace Agenda.Infrastructure.Database.Repositories.Agenda
{
    public class AgendaRepository : GenericRepository<AgendaEntity, AgendaModel>, IAgendaRepository
    {
        public AgendaRepository(AgendaDbContext context) : base(context) { }

        protected override AgendaEntity ToEntity(AgendaModel model) => model.ToEntity();
        protected override AgendaModel ToModel(AgendaEntity entity) => entity.ToModel(entity.User.ToModel());

        public async Task<IEnumerable<AgendaModel>> GetByUserIdAsync(Guid userId)
        {
            var entities = await _dbSet.AsNoTracking()
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return entities.Select(a => ToModel(a));
        }
    }
}