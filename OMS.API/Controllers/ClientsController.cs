using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.StoredProcedureParams;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing clients data.
    /// </summary>
    [Authorize]
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
        /// Creates a new entity.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/entities
        ///     {
        ///         "name": "New Entity",
        ///         "description": "Entity description"
        ///     }
        /// </remarks>
        /// <param name="dto">The DTO containing data for the new entity</param>
        /// <returns>The created entity with generated ID</returns>
        /// <response code="201">Returns the newly created entity</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("addwithaccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<ClientAccountDto>> AddWithAccountAsync([FromBody] ClientAccountDto dto)
        {
            try
            {
                var model = _mapper.Map<ClientAccountModel>(dto);
                var isSuccess = await _service.AddWithAccountAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save entity in the database",
                        Errors = { { "General", new[] { "Failed to save entity in the database" } } }
                    });
                }

                dto.Client.Id = model.Client.Id;
                dto.Account.Id = model.Account.Id;

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating entity",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/entities
        ///     {
        ///         "name": "New Entity",
        ///         "description": "Entity description"
        ///     }
        /// </remarks>
        /// <param name="dto">The DTO containing data for the new entity</param>
        /// <returns>The created entity with generated ID</returns>
        /// <response code="201">Returns the newly created entity</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("updatewithaccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> UpdateWithAccountAsync([FromBody] ClientAccountDto dto)
        {
            try
            {
                var model = _mapper.Map<ClientAccountModel>(dto);
                var isSuccess = await _service.UpdateWithAccountAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save entity in the database",
                        Errors = { { "General", new[] { "Failed to save entity in the database" } } }
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating entity",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
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
        [Authorize(Policy = PermissionsData.Clients.View)]
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
        [Authorize(Policy = PermissionsData.Clients.View)]
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


        /// <summary>
        /// Pay all Debts by Client Id.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// Patch /api/clients/pay
        /// </remarks>
        /// <param name="PayDebtsDto">The data transfare object.</param>
        /// <returns>Returns operation result.</returns>
        /// <response code="200">Debt paid successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("pay")]
        [Authorize(Policy = PermissionsData.Clients.PayDebts)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> PayDebtAsync([FromBody] PayDebtsDto PayDebtsDto)
        {
            try
            {
                var isExist = await _service.IsExistAsync(PayDebtsDto.ClientId);
                if (!isExist) return NotFound();

                var payDebtsModel = _mapper.Map<PayDebtsModel>(PayDebtsDto);

                await _service.PayAllDebtsById(payDebtsModel);

                return Ok((int)payDebtsModel.PayDebtStatus);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Server Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #region override abstract Methods
        protected override int GetModelId(ClientModel model) => model.Id;
        protected override void SetDtoId(ClientDto dto, int id) => dto.Id = id;
        protected override async Task<PagedResult<ClientModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<ClientModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(ClientModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(ClientModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, ClientDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}

