using Agenda.Domain.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Infrastructure.Database.Context;

namespace Agenda.Infrastructure.Database.Repositories.Common;

public abstract class GenericRepository<TEntity, TModel> : IGenericRepository<TModel>
    where TEntity : class
    where TModel : class
{
    protected readonly AgendaDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AgendaDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    protected abstract TEntity ToEntity(TModel model);
    protected abstract TModel ToModel(TEntity entity);

    public async Task<TModel?> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity != null ? ToModel(entity) : null;
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        var entities = await _dbSet.AsNoTracking().ToListAsync();
        return entities.Select(ToModel);
    }

    public async Task AddAsync(TModel model)
    {
        var entity = ToEntity(model);
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TModel model)
    {
        var entity = ToEntity(model);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}