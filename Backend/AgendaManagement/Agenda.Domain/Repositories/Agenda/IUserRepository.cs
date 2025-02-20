using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Common;

namespace Agenda.Domain.Repositories.Agenda;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<UserModel?> GetByEmailAsync(string email);
}