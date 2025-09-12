using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Dtos.Hybrid;
using OMS.Common.Dtos.StoredProcedureParams;
using OMS.Common.Dtos.Tables;
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
        /// Adds a new client with an associated account.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/clients/addwithaccount
        /// {
        ///   "client": { ... },
        ///   "account": { ... }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="dto">The client and account data transfer object.</param>
        /// <returns>The created client and account with generated IDs.</returns>
        /// <response code="200">Client and account created successfully.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("addwithaccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Updates an existing client and its associated account.
        /// </summary>
        /// <remarks>
        /// <code>
        /// PUT /api/clients/updatewithaccount
        /// {
        ///   "client": { ... },
        ///   "account": { ... }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="dto">The client and account data transfer object.</param>
        /// <returns>Returns 200 OK if update succeeded.</returns>
        /// <response code="200">Client and account updated successfully.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("updatewithaccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWithAccountAsync([FromBody] ClientAccountDto dto)
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
        /// Retrieves a client by its person ID.
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/clients/by-person/123
        /// </code>
        /// </remarks>
        /// <param name="personId">The person ID of the client to retrieve (must be positive integer).</param>
        /// <returns>The requested client data.</returns>
        /// <response code="200">Client found and returned.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Internal server error.</response>
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
        /// Retrieves a client ID by its person ID.
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/clients/{personId}/id
        /// </code>
        /// </remarks>
        /// <param name="personId">The person ID to retrieve the client ID for (must be positive integer).</param>
        /// <returns>The client ID.</returns>
        /// <response code="200">Client ID found and returned.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Internal server error.</response>
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
        /// Pays all debts for a client by client ID.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/clients/pay
        /// {
        ///   "clientId": 123,
        ///   "notes": "Payment for all debts",
        ///   "createdByUserId": 1
        /// }
        /// </code>
        /// </remarks>
        /// <param name="payDebtsDto">The pay debts data transfer object.</param>
        /// <returns>Status code representing the result of the operation.</returns>
        /// <response code="200">Debts paid successfully (returns status code as integer).</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("pay")]
        [Authorize(Policy = PermissionsData.Clients.PayDebts)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> PayDebtAsync([FromBody] PayDebtsDto payDebtsDto)
        {
            if (payDebtsDto.ClientId <= 0) return NotFound();

            try
            {
                var isExist = await _service.IsExistAsync(payDebtsDto.ClientId);
                if (!isExist) return NotFound();

                var payDebtsModel = _mapper.Map<PayDebtsModel>(payDebtsDto);

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

