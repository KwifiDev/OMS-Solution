using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Dtos.StoredProcedureParams;
using OMS.Common.Dtos.Tables;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing accounts.
    /// </summary>
    [Authorize]
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : GenericController<IAccountService, AccountDto, AccountModel>
    {

        /// <summary>
        /// Initializes a new instance of the AccountsController class.
        /// </summary>
        /// <param name="accountService">The account service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public AccountsController(IAccountService accountService, IMapper mapper) : base(accountService, mapper)
        {
        }

        /// <summary>
        /// Retrieves an account using the provided client ID.
        /// </summary>
        /// <remarks>
        /// <b>Sample request:</b>
        /// <code>
        /// GET /api/accounts/by-client/123
        /// </code>
        /// </remarks>
        /// <param name="clientId">The ID of the client whose account information is requested.</param>
        /// <returns>Returns account details if found, otherwise appropriate status code.</returns>
        /// <response code="200">Account found and returned successfully.</response>
        /// <response code="400">Client ID is invalid (must be a positive integer).</response>
        /// <response code="204">No account found for the given client ID.</response>
        /// <response code="500">Internal server error occurred.</response>
        [HttpGet("by-client/{clientId:int}")]
        [Authorize(Policy = PermissionsData.Accounts.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountDto>> GetByClientIdAsync([FromRoute] int clientId)
        {
            if (clientId <= 0)
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid Client ID",
                    Detail = "Client ID must be a positive integer.",
                    Status = StatusCodes.Status400BadRequest
                });

            try
            {
                var model = await _service.GetByClientIdAsync(clientId);
                return model is null ? NoContent() : Ok(_mapper.Map<AccountDto>(model));
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
        /// Executes a transaction (Deposit, Withdraw, Transfer) on an account.
        /// </summary>
        /// <remarks>
        /// <b>Sample request:</b>
        /// <code>
        /// POST /api/accounts/transactions
        /// Content-Type: application/json
        /// {
        ///  "accountId": 98765,
        ///  "amount": 150.75,
        ///   "transactionType": 1
        /// }
        /// </code>
        /// </remarks>
        /// <param name="dto">The data transfer object containing transaction details.</param>
        /// <returns>Returns the transaction result and status.</returns>
        /// <response code="200">Transaction processed successfully (status in response body).</response>
        /// <response code="404">Account not found for the given AccountId.</response>
        /// <response code="400">Request is invalid (e.g., missing or invalid fields).</response>
        /// <response code="500">Internal server error occurred.</response>
        [HttpPost("transactions")]
        [Authorize(Policy = PermissionsData.Accounts.Transaction)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountTransactionDto>> StartTransactionAsync([FromBody] AccountTransactionDto dto)
        {
            if (dto.AccountId <= 0) return NotFound();

            try
            {
                var model = _mapper.Map<AccountTransactionModel>(dto);

                bool isSuccess = await _service.StartTransactionAsync(model);
                dto.TransactionStatus = model.TransactionStatus;
                return Ok(dto);
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
        protected override int GetModelId(AccountModel model) => model.Id;
        protected override void SetDtoId(AccountDto dto, int id) => dto.Id = id;
        protected override async Task<PagedResult<AccountModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<AccountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(AccountModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(AccountModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, AccountDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
