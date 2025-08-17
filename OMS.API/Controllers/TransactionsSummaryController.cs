using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.Common.Data;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/transactionssummary")]
    [ApiController]
    public class TransactionsSummaryController : GenericViewController<ITransactionsSummaryService, TransactionsSummaryDto, TransactionsSummaryModel>
    {
        public TransactionsSummaryController(ITransactionsSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }

        /// <summary>
        /// Retrieves all TransactionsSummary by Account Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/transactionssummary/accounts/123/transactions
        ///     
        /// Returns all available transactionssummary by Account Id in the system.
        /// </remarks>
        /// <returns>List of all transactions by account</returns>
        /// <response code="200">Returns the complete list of transactions</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("accounts/{accountId:int}/transactions")]
        [Authorize(Policy = PermissionsData.TransactionsSummary.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TransactionsSummaryDto>>> GetTransactionsByAccountIdAsync([FromRoute] int accountId)
        {
            try
            {
                var models = await _service.GetTransactionsByAccountIdAsync(accountId);
                return Ok(_mapper.Map<IEnumerable<TransactionsSummaryDto>>(models));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving entities",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        #region override abstract Methods 
        protected override async Task<IEnumerable<TransactionsSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<TransactionsSummaryModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion

    }
}
