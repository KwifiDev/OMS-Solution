using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class UserDetailRepository : GenericViewRepository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(AppDbContext context) : base(context)
        {
        }

    }
}
