using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : BaseController<IUserService, UserDto, UserModel>
    {
        public UsersController(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }

        /// <summary>
        /// Gets the unique identifier from the UserModel.
        /// </summary>
        /// <param name="model">The UserModel instance.</param>
        /// <returns>The user's identifier.</returns>
        protected override int GetModelId(UserModel model) => model.UserId;

        /// <summary>
        /// Sets the identifier in the UserDto.
        /// </summary>
        /// <param name="dto">The UserDto instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected override void SetDtoId(UserDto dto, int id) => dto.UserId = id;

        /// <summary>
        /// Retrieves all users from the service.
        /// </summary>
        /// <returns>A collection of UserModel instances.</returns>
        protected override async Task<IEnumerable<UserModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The requested UserModel or null if not found.</returns>
        protected override async Task<UserModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="model">The UserModel to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> AddModelAsync(UserModel model) => await _service.AddAsync(model);

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="model">The UserModel with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> UpdateModelAsync(UserModel model) => await _service.UpdateAsync(model);

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Verifies that the ID matches the user ID in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The UserDto containing the user ID.</param>
        /// <returns>True if the IDs match, otherwise false.</returns>
        protected override bool IsIdentifierIdentical(int id, UserDto dto) => id == dto.UserId;

        /// <summary>
        /// Check a user from the database.
        /// </summary>
        /// <param name="id">The ID of the user to check if is exist.</param>
        /// <returns>True if the user exist, otherwise false.</returns>
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
    }
}
