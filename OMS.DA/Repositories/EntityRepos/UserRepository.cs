using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(AppDbContext context) : base(context)
        {
            _users = context.Set<User>();
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _users.Where(u => u.Username == username && u.Password == password).AsNoTracking().SingleOrDefaultAsync();
        }
    }
}
