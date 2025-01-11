using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Repositories.EntityRepos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
    }
}
