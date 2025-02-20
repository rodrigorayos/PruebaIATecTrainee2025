using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Infrastructure.Database.Entities.Agenda;
using Agenda.Infrastructure.Database.Extensions.Agenda;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Database.Repositories.Agenda;

public class EventRepository : IEventRepository
{
    private readonly AgendaDbContext _context;

    public EventRepository(AgendaDbContext context)
    {
        _context = context;
    }

    public async Task<EventModel> CreateAsync(EventModel model)
    {
        var entity = model.ToEntity();
        _context.Set<EventEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task<EventModel?> GetByIdAsync(int id)
    {
        var entity = await _context.Set<EventEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
            
        return entity?.ToModel();
    }

    public async Task<List<EventModel>> GetAllAsync()
    {
        var entities = await _context.Set<EventEntity>()
            .AsNoTracking()
            .ToListAsync();
            
        return entities.ToModelList();
    }

    public async Task<EventModel> UpdateAsync(EventModel model)
    {
        var entity = await _context.Set<EventEntity>().FindAsync(model.Id);
        
        if (entity == null)
        {
            throw new KeyNotFoundException($"Event with ID {model.Id} not found");
        }

        entity.Name = model.Name;
        entity.Description = model.Description;
        entity.Date = model.Date;
        entity.Location = model.Location;
        entity.Participants = string.Join(",", model.Participants);

        await _context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Set<EventEntity>().FindAsync(id);
        
        if (entity == null)
        {
            return false;
        }

        _context.Set<EventEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<EventModel>> GetByDateAsync(DateTime date)
    {
        var entities = await _context.Set<EventEntity>()
            .AsNoTracking()
            .Where(e => e.Date.Date == date.Date)
            .ToListAsync();
            
        return entities.ToModelList();
    }
}