using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PersonService : GenericService<Person, PersonModel>, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IGenericRepository<Person> genericRepo,
                             IMapperService mapper,
                             IPersonRepository repository) : base(genericRepo, mapper)
        {
            _personRepository = repository;
        }

    }
}
