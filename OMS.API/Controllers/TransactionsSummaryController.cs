using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    [Route("api/transactionssummary")]
    [ApiController]
    public class TransactionsSummaryController : GenericViewController<ITransactionsSummaryService, TransactionsSummaryDto, TransactionsSummaryModel>
    {
        public TransactionsSummaryController(ITransactionsSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }

        protected override async Task<IEnumerable<TransactionsSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        protected override async Task<TransactionsSummaryModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);


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

    }
}
