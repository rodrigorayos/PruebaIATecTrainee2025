﻿using Agenda.Domain.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Database.Repositories.Common;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AgendaDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AgendaDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }
}