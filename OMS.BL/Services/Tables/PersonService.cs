using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonModel>> GetAllPeopleAsync()
        {
            IEnumerable<Person> people = await _repository.GetAllAsync();

            return people?.Select(p => new PersonModel
            {
                PersonId = p.PersonId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                Phone = p.Phone

            }) ?? Enumerable.Empty<PersonModel>();
        }

        public async Task<PersonModel?> GetPersonByIdAsync(int personId)
        {
            Person? person = await _repository.GetByIdAsync(personId);

            return person == null ? null : new PersonModel
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                Phone = person.Phone
            };
        }

        public async Task<bool> AddPersonAsync(PersonModel model)
        {
            if (model == null) return false;

            Person person = new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Phone = model.Phone,
            };

            bool success = await _repository.AddAsync(person);

            if (success) model.PersonId = person.PersonId;

            return success;
        }

        public async Task<bool> UpdatePersonAsync(PersonModel model)
        {
            if (model == null) return false;

            Person? person = await _repository.GetByIdAsync(model.PersonId);

            if (person == null) return false;

            person.FirstName = model.FirstName;
            person.LastName = model.LastName;
            person.Gender = model.Gender;
            person.Phone = model.Phone;

            return await _repository.UpdateAsync(person);
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            if (personId <= 0) return false;

            return await _repository.DeleteAsync(personId);
        }
    }
}
