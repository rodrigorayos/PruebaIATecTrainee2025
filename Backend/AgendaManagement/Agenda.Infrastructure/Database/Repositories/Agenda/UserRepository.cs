using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Infrastructure.Database;
using Agenda.Infrastructure.Database.Entities.Agenda;
using Agenda.Infrastructure.Database.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Agenda.Infrastructure.Database.Context;
using Agenda.Infrastructure.Database.Extensions.Agenda;

namespace Agenda.Infrastructure.Database.Repositories.Agenda
{
    public class UserRepository : GenericRepository<UserEntity, UserModel>, IUserRepository
    {
        public UserRepository(AgendaDbContext context) : base(context) { }

        protected override UserEntity ToEntity(UserModel model) => model.ToEntity();
        protected override UserModel ToModel(UserEntity entity) => entity.ToModel();

        public async Task<UserModel?> GetByEmailAsync(string email)
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return entity != null ? ToModel(entity) : null;
        }
    }
}