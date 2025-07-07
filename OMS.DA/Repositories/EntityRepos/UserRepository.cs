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

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _users.AsNoTracking()
                  .Select(u => new User
                  {
                      UserId = u.UserId,
                      BranchId = u.BranchId,
                      PersonId = u.PersonId,
                      Username = u.Username,
                      Permissions = u.Permissions
                  })
                  .Where(u => u.UserId == id)
                  .FirstOrDefaultAsync();
        }

        public override async Task<bool> UpdateAsync(User user)
        {
            int rawAffected = await _users.Where(u => u.UserId == user.UserId)
                                          .ExecuteUpdateAsync(setters => setters
                                          .SetProperty(u => u.Username, user.Username)
                                          .SetProperty(u => u.BranchId, user.BranchId));

            return rawAffected > 0;
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

        public async Task<User?> GetByPersonIdAsync(int personId)
        {
            return await _users.AsNoTracking()
                .Select(u => new User
                {
                    UserId = u.UserId,
                    BranchId = u.BranchId,
                    PersonId = u.PersonId,
                    Username = u.Username,
                    Permissions = u.Permissions
                })
                .Where(u => u.PersonId == personId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            return await _users.Where(u => u.PersonId == personId).Select(u => u.UserId).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserActive(int userId)
            => await _users.AsNoTracking()
                           .Where(c => c.UserId == userId)
                           .Select(u => u.IsActive)
                           .FirstOrDefaultAsync();

        public async Task<bool> UpdateUserActivationStatus(int userId, bool isActive)
        {
            var rowAffected = await _users.Where(u => u.UserId == userId)
                                          .ExecuteUpdateAsync(u => u.SetProperty(c => c.IsActive, isActive));

            return rowAffected > 0;
        }

        public async Task<bool> IsUsernameUsedAsync(int userId, string username)
        {
            return await _users.AsNoTracking()
                               .Where(u => u.UserId != userId)
                               .Select(u => u.Username)
                               .AnyAsync(us => us == username);
        }

        public async Task<string?> GetUsernamebyId(int userId)
        {
            return await _users.AsNoTracking()
                               .Where(u => u.UserId == userId)
                               .Select(u => u.Username)
                               .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePassword(int userId, string newPassword)
        {
            int rawAffected = await _users.Where(u => u.UserId == userId)
                                          .ExecuteUpdateAsync(u => u.SetProperty(p => p.Password, newPassword));

            return rawAffected > 0;
        }

        public async Task<string?> GetPasswordById(int userId)
            => await _users.Where(u => u.UserId == userId)
                           .Select(u => u.Password)
                           .FirstOrDefaultAsync();
        
    }
}
