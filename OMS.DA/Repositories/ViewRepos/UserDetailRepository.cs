using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class UserDetailRepository : GenericViewRepository<UserDetail>, IUserDetailRepository
    {
        private readonly DbSet<UserDetail> _userDetails;

        public UserDetailRepository(AppDbContext context) : base(context)
        {
            _userDetails = context.Set<UserDetail>();
        }

    }
}
