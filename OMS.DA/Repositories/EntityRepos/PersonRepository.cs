using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Repositories.EntityRepos
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {

        }
    }
}
