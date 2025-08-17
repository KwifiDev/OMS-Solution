using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.StoredProcedureParams;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Data;

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
        /// **Sample request:**
        /// ```
        /// GET /api/accounts/by-client/123
        /// ```
        /// </remarks>
        /// <param name="clientId">The ID of the client whose account information is requested.</param>
        /// <returns>Returns account details if found, otherwise an error message.</returns>
        /// <response code="200">Returns the requested account details.</response>
        /// <response code="400">If the client ID provided is invalid.</response>
        /// <response code="404">If no account is found with the given client ID.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("by-client/{clientId:int}")]
        [Authorize(Policy = PermissionsData.Accounts.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                return model is null ? NotFound() : Ok(_mapper.Map<AccountDto>(model));
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
        /// **Sample request:**
        /// ```
        /// POST /api/accounts/transactions
        /// Content-Type: application/json
        ///
        /// {
        ///   "accountId": 98765,
        ///   "amount": 150.75,
        ///   "transactionType": "Deposit"
        /// }
        /// ```
        /// </remarks>
        /// <param name="dto">The data transfer object containing transaction details.</param>
        /// <returns>Returns the status of the transaction.</returns>
        /// <response code="200">Returns the transaction result if successful.</response>
        /// <response code="400">If the request is invalid (missing account ID, invalid amount, etc.).</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("transactions")]
        [Authorize(Policy = PermissionsData.Accounts.Transaction)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountTransactionDto>> StartTransactionAsync([FromBody] AccountTransactionDto dto)
        {
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
        protected override int GetModelId(AccountModel model) => model.AccountId;
        protected override void SetDtoId(AccountDto dto, int id) => dto.AccountId = id;
        protected override async Task<IEnumerable<AccountModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<AccountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(AccountModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(AccountModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, AccountDto dto) => id == dto.AccountId;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
