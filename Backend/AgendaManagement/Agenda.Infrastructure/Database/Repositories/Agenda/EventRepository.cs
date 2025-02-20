﻿using Agenda.Domain.Models.Agenda;
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
    public class EventRepository : GenericRepository<EventEntity, EventModel>, IEventRepository
    {
        public EventRepository(AgendaDbContext context) : base(context) { }

        protected override EventEntity ToEntity(EventModel model) => model.ToEntity();
        protected override EventModel ToModel(EventEntity entity) => entity.ToModel(entity.Agenda.ToModel(entity.Agenda.User.ToModel()));

        public async Task<IEnumerable<EventModel>> GetByAgendaIdAsync(Guid agendaId)
        {
            var entities = await _dbSet.AsNoTracking()
                .Where(e => e.AgendaId == agendaId)
                .ToListAsync();
            return entities.Select(e => ToModel(e));
        }
    }
}