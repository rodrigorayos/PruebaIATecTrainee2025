using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Domain.Repositories.Common;

public interface IGenericRepository<TModel> where TModel : class
{
    Task<TModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<TModel>> GetAllAsync();
    Task AddAsync(TModel model);
    Task UpdateAsync(TModel model);
    Task DeleteAsync(Guid id);
}