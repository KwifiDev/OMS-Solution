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
            return await _users.AsNoTracking()
                        .Where(u => u.Username == username && u.Password == password)
                        .Select(u => new User
                        {
                            UserId = u.UserId,
                            PersonId = u.PersonId,
                            BranchId = u.BranchId,
                            Username = u.Username,
                            Permissions = u.Permissions,
                            IsActive = u.IsActive,
                            Person = new Person
                            {
                                FirstName = u.Person.FirstName,
                                LastName = u.Person.LastName,
                                Gender = u.Person.Gender
                            }
                        })
                        .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserLoginByPersonIdAsync(int personId)
        {
            return await _users.AsNoTracking()
                       .Where(u => u.PersonId == personId)
                       .Select(u => new User
                       {
                           UserId = u.UserId,
                           PersonId = u.PersonId,
                           BranchId = u.BranchId,
                           Username = u.Username,
                           Permissions = u.Permissions,
                           IsActive = u.IsActive,
                           Person = new Person
                           {
                               FirstName = u.Person.FirstName,
                               LastName = u.Person.LastName,
                               Gender = u.Person.Gender
                           }
                       })
                       .FirstOrDefaultAsync();
        }
    }
}
