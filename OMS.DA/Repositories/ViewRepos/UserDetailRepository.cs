using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class UserDetailRepository : GenericViewRepository<UserDetail>, IUserDetailRepository
    {
        private readonly DbSet<UserDetail> _userDetails;

        public UserDetailRepository(AppDbContext context) : base(context)
        {
            _userDetails = context.Set<UserDetail>();
        }

        /*
                 public async Task<UserDetail?> GetUserDetailByIdAsync(int userId)
        {
            return await _userDetails
                        .AsNoTracking()
                        .Where(m => m.UserId == userId)
                        .SingleOrDefaultAsync();
        }
         */
    }
}
