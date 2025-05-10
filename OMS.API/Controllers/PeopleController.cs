using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing people data.
    /// </summary>
    [Route("api/people")]
    [ApiController]
    public class PeopleController : GenericController<IPersonService, PersonDto, PersonModel>
    {
        /// <summary>
        /// Initializes a new instance of the PeopleController class.
        /// </summary>
        /// <param name="personService">The person service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public PeopleController(IPersonService personService, IMapper mapper)
            : base(personService, mapper)
        {
        }

        /// <summary>
        /// Gets the unique identifier from the PersonModel.
        /// </summary>
        /// <param name="model">The PersonModel instance.</param>
        /// <returns>The person's identifier.</returns>
        protected override int GetModelId(PersonModel model) => model.PersonId;

        /// <summary>
        /// Sets the identifier in the PersonDto.
        /// </summary>
        /// <param name="dto">The PersonDto instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected override void SetDtoId(PersonDto dto, int id) => dto.PersonId = id;

        /// <summary>
        /// Retrieves all people from the service.
        /// </summary>
        /// <returns>A collection of PersonModel instances.</returns>
        protected override async Task<IEnumerable<PersonModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>The requested PersonModel or null if not found.</returns>
        protected override async Task<PersonModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);

        /// <summary>
        /// Adds a new person to the database.
        /// </summary>
        /// <param name="model">The PersonModel to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> AddModelAsync(PersonModel model) => await _service.AddAsync(model);

        /// <summary>
        /// Updates an existing person in the database.
        /// </summary>
        /// <param name="model">The PersonModel with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> UpdateModelAsync(PersonModel model) => await _service.UpdateAsync(model);

        /// <summary>
        /// Deletes a person from the database.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Verifies that the ID matches the person ID in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The PersonDto containing the person ID.</param>
        /// <returns>True if the IDs match, otherwise false.</returns>
        protected override bool IsIdentifierIdentical(int id, PersonDto dto) => id == dto.PersonId;

        /// <summary>
        /// Check a person from the database.
        /// </summary>
        /// <param name="id">The ID of the person to check if is exist.</param>
        /// <returns>True if the person exist, otherwise false.</returns>
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
    }
}

