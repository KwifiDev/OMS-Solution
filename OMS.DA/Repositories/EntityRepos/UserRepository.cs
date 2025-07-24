using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                  .Select(u => new User
                  {
                      Id = u.Id,
                      BranchId = u.BranchId,
                      PersonId = u.PersonId,
                      UserName = u.UserName
                  })
                  .Where(u => u.Id == id)
                  .FirstOrDefaultAsync();
        }

        public override async Task<bool> UpdateAsync(User user)
        {
            int rawAffected = await _dbSet.Where(u => u.Id == user.Id)
                                          .ExecuteUpdateAsync(p => p
                                          .SetProperty(u => u.BranchId, user.BranchId));

            return rawAffected > 0;
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _dbSet.AsNoTracking()
                        .Where(u => u.UserName == username && u.PasswordHash == password)
                        .Select(u => new User
                        {
                            Id = u.Id,
                            PersonId = u.PersonId,
                            BranchId = u.BranchId,
                            UserName = u.UserName,
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
            return await _dbSet.AsNoTracking()
                       .Where(u => u.PersonId == personId)
                       .Select(u => new User
                       {
                           Id = u.Id,
                           PersonId = u.PersonId,
                           BranchId = u.BranchId,
                           UserName = u.UserName,
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
            return await _dbSet.AsNoTracking()
                .Select(u => new User
                {
                    Id = u.Id,
                    BranchId = u.BranchId,
                    PersonId = u.PersonId,
                    UserName = u.UserName
                })
                .Where(u => u.PersonId == personId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            return await _dbSet.Where(u => u.PersonId == personId).Select(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserActive(int userId)
            => await _dbSet.AsNoTracking()
                           .Where(c => c.Id == userId)
                           .Select(u => u.IsActive)
                           .FirstOrDefaultAsync();

        public async Task<bool> UpdateUserActivationStatus(int userId, bool isActive)
        {
            var rowAffected = await _dbSet.Where(u => u.Id == userId)
                                          .ExecuteUpdateAsync(u => u.SetProperty(c => c.IsActive, isActive));

            return rowAffected > 0;
        }

        public async Task<bool> IsUsernameUsedAsync(int userId, string username)
        {
            return await _dbSet.AsNoTracking()
                               .Where(u => u.Id != userId)
                               .Select(u => u.UserName)
                               .AnyAsync(us => us == username);
        }

        public async Task<string?> GetUsernamebyId(int userId)
        {
            return await _dbSet.AsNoTracking()
                               .Where(u => u.Id == userId)
                               .Select(u => u.UserName)
                               .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePassword(int userId, string newPassword)
        {
            int rawAffected = await _dbSet.Where(u => u.Id == userId)
                                          .ExecuteUpdateAsync(u => u.SetProperty(p => p.PasswordHash, newPassword));

            return rawAffected > 0;
        }

        public async Task<string?> GetPasswordById(int userId)
            => await _dbSet.Where(u => u.Id == userId)
                           .Select(u => u.PasswordHash)
                           .FirstOrDefaultAsync();

    }
}
