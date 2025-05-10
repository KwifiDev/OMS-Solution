using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing clients data.
    /// </summary>
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : GenericController<IClientService, ClientDto, ClientModel>
    {
        /// <summary>
        /// Initializes a new instance of the ClientsController class.
        /// </summary>
        /// <param name="clientService">The client service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public ClientsController(IClientService clientService, IMapper mapper)
            : base(clientService, mapper)
        {
        }

        /// <summary>
        /// Gets the unique identifier from the ClientModel.
        /// </summary>
        /// <param name="model">The ClientModel instance.</param>
        /// <returns>The client's identifier.</returns>
        protected override int GetModelId(ClientModel model) => model.ClientId;

        /// <summary>
        /// Sets the identifier in the ClientDto.
        /// </summary>
        /// <param name="dto">The ClientDto instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected override void SetDtoId(ClientDto dto, int id) => dto.ClientId = id;

        /// <summary>
        /// Retrieves all clients from the service.
        /// </summary>
        /// <returns>A collection of ClientModel instances.</returns>
        protected override async Task<IEnumerable<ClientModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific client by their ID.
        /// </summary>
        /// <param name="id">The ID of the client to retrieve.</param>
        /// <returns>The requested ClientModel or null if not found.</returns>
        protected override async Task<ClientModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);

        /// <summary>
        /// Adds a new client to the database.
        /// </summary>
        /// <param name="model">The ClientModel to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> AddModelAsync(ClientModel model) => await _service.AddAsync(model);

        /// <summary>
        /// Updates an existing client in the database.
        /// </summary>
        /// <param name="model">The ClientModel with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> UpdateModelAsync(ClientModel model) => await _service.UpdateAsync(model);

        /// <summary>
        /// Deletes a client from the database.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Verifies that the ID matches the client ID in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The ClientDto containing the client ID.</param>
        /// <returns>True if the IDs match, otherwise false.</returns>
        protected override bool IsIdentifierIdentical(int id, ClientDto dto) => id == dto.ClientId;

        /// <summary>
        /// Check a client from the database.
        /// </summary>
        /// <param name="id">The ID of the client to check if is exist.</param>
        /// <returns>True if the client exist, otherwise false.</returns>
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
    }
}

