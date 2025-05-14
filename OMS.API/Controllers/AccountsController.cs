using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.StoredProcedureParams;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing people data.
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : GenericController<IAccountService, AccountDto, AccountModel>
    {
        /// <summary>
        /// Initializes a new instance of the AccountsController class.
        /// </summary>
        /// <param name="accountService">The account service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public AccountsController(IAccountService accountService, IMapper mapper)
            : base(accountService, mapper)
        {
        }


        /// <summary>
        /// Retrieves a specific Account by client ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/account/clientid/123
        /// </remarks>
        /// <param name="id">The Client Id of the account to retrieve</param>
        /// <returns>The requested account</returns>
        /// <response code="200">Returns the requested account</response>
        /// <response code="404">If account was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("clientid/{clientId:int}")]
        [ActionName("GetByClientId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountDto>> GetByClientIdAsync([FromRoute] int clientId)
        {
            if (clientId <= 0) return NotFound();

            try
            {
                var model = await _service.GetByClientIdAsync(clientId);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<AccountDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving account",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// make a deposit transaction to an account.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/accounts/deposit
        /// </remarks>
        /// <param name="dto">The DTO containing data for the transaction</param>
        /// <returns>The created entity with generated ID</returns>
        /// <response code="201">Returns the transaction status for the account</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("deposit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountTransactionDto>> DepositIntoAccountAsync([FromBody] AccountTransactionDto dto)
        {
            try
            {
                var model = _mapper.Map<AccountTransactionModel>(dto);
                var isSuccess = await _service.DepositIntoAccountAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to make a transaction in the database",
                        Errors = { { "General", new[] { "Failed to make a transaction to account in the database" } } }
                    });
                }

                dto.TransactionStatus = model.TransactionStatus;

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error Deposit Into Account",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }



        /// <summary>
        /// make a withdraw transaction to an account.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/accounts/withdraw
        /// </remarks>
        /// <param name="dto">The DTO containing data for the transaction</param>
        /// <returns>The created entity with generated ID</returns>
        /// <response code="201">Returns the transaction status for the account</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("withdraw")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountTransactionDto>> WithdrawIntoAccountAsync([FromBody] AccountTransactionDto dto)
        {
            try
            {
                var model = _mapper.Map<AccountTransactionModel>(dto);
                var isSuccess = await _service.WithdrawFromAccountAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to make a transaction in the database",
                        Errors = { { "General", new[] { "Failed to make a transaction to account in the database" } } }
                    });
                }

                dto.TransactionStatus = model.TransactionStatus;

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error Withdraw From Account",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// Gets the unique identifier from the AccountModel.
        /// </summary>
        /// <param name="model">The AccountModel instance.</param>
        /// <returns>The account's identifier.</returns>
        protected override int GetModelId(AccountModel model) => model.AccountId;

        /// <summary>
        /// Sets the identifier in the AccountDto.
        /// </summary>
        /// <param name="dto">The AccountDto instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected override void SetDtoId(AccountDto dto, int id) => dto.AccountId = id;

        /// <summary>
        /// Retrieves all people from the service.
        /// </summary>
        /// <returns>A collection of AccountModel instances.</returns>
        protected override async Task<IEnumerable<AccountModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific account by their ID.
        /// </summary>
        /// <param name="id">The ID of the account to retrieve.</param>
        /// <returns>The requested AccountModel or null if not found.</returns>
        protected override async Task<AccountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);

        /// <summary>
        /// Adds a new account to the database.
        /// </summary>
        /// <param name="model">The AccountModel to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> AddModelAsync(AccountModel model) => await _service.AddAsync(model);

        /// <summary>
        /// Updates an existing account in the database.
        /// </summary>
        /// <param name="model">The AccountModel with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> UpdateModelAsync(AccountModel model) => await _service.UpdateAsync(model);

        /// <summary>
        /// Deletes a account from the database.
        /// </summary>
        /// <param name="id">The ID of the account to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Verifies that the ID matches the account ID in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The AccountDto containing the account ID.</param>
        /// <returns>True if the IDs match, otherwise false.</returns>
        protected override bool IsIdentifierIdentical(int id, AccountDto dto) => id == dto.AccountId;

        /// <summary>
        /// Check a account from the database.
        /// </summary>
        /// <param name="id">The ID of the account to check if is exist.</param>
        /// <returns>True if the account exist, otherwise false.</returns>
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
    }
}

