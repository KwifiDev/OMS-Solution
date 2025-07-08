using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class PersonDetailRepository : GenericViewRepository<PersonDetail>, IPersonDetailRepository
    {
        public PersonDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
