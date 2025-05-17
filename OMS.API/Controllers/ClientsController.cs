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
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/clients/by-person/123
        /// </remarks>
        /// <param name="personId">The person ID of the entity to retrieve (must be positive integer)</param>
        /// <returns>The requested client Dto</returns>
        /// <response code="200">Returns the requested entity</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("by-person/{personId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                var model = await _service.GetByPersonIdAsync(personId);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<ClientDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving client",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/clients/123/id
        ///     123 is the personId
        /// </remarks>
        /// <param name="personId">The person ID to retrieve client ID (must be positive integer)</param>
        /// <returns>The requested client Id</returns>
        /// <response code="200">Returns the requested id</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{personId:int}/id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetIdByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                int clientId = await _service.GetIdByPersonIdAsync(personId);
                return clientId is -1 ? NotFound() : Ok(clientId);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving clientId",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        #region override abstract Methods
        protected override int GetModelId(ClientModel model) => model.ClientId;
        protected override void SetDtoId(ClientDto dto, int id) => dto.ClientId = id;
        protected override async Task<IEnumerable<ClientModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<ClientModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(ClientModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(ClientModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, ClientDto dto) => id == dto.ClientId;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}

