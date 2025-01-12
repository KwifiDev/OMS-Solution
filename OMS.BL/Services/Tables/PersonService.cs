using OMS.BL.IServices.Tables;
using OMS.BL.Dtos.Tables;
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

        public async Task<IEnumerable<PersonDto>> GetAllPeopleAsync()
        {
            IEnumerable<Person> people = await _repository.GetAllAsync();

            return people?.Select(p => new PersonDto
            {
                PersonId = p.PersonId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                Phone = p.Phone

            }) ?? Enumerable.Empty<PersonDto>();
        }

        public async Task<PersonDto?> GetPersonByIdAsync(int personId)
        {
            Person? person = await _repository.GetByIdAsync(personId);

            return person == null ? null : new PersonDto
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                Phone = person.Phone
            };
        }

        public async Task<bool> AddPersonAsync(PersonDto dto)
        {
            if (dto == null) return false;

            Person person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Phone = dto.Phone,
            };

            bool success = await _repository.AddAsync(person);

            if (success) dto.PersonId = person.PersonId;

            return success;
        }

        public async Task<bool> UpdatePersonAsync(PersonDto dto)
        {
            if (dto == null) return false;

            Person? person = await _repository.GetByIdAsync(dto.PersonId);

            if (person == null) return false;

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.Gender = dto.Gender;
            person.Phone = dto.Phone;

            return await _repository.UpdateAsync(person);
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            if (personId <= 0) return false;

            return await _repository.DeleteAsync(personId);
        }
    }
}
