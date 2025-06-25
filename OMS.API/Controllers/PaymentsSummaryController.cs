using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    [Route("api/paymentssummary")]
    [ApiController]
    public class PaymentsSummaryController : GenericViewController<IPaymentsSummaryService, PaymentsSummaryDto, PaymentsSummaryModel>
    {
        public PaymentsSummaryController(IPaymentsSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }

        /// <summary>
        /// Retrieves all PaymentsSummary by Account Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/paymentssummary/accounts/123/payments
        ///     
        /// Returns all available payments summary by Account Id in the system.
        /// </remarks>
        /// <returns>List of all payments by account</returns>
        /// <response code="200">Returns the complete list of payments</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("accounts/{accountId:int}/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentsSummaryDto>>> GetPaymentsByAccountIdAsync([FromRoute] int accountId)
        {
            try
            {
                var models = await _service.GetPaymentsByAccountIdAsync(accountId);
                return Ok(_mapper.Map<IEnumerable<PaymentsSummaryDto>>(models));
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
        protected override async Task<IEnumerable<PaymentsSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<PaymentsSummaryModel?> GetModelByIdAsync(int paymentId) => await _service.GetByIdAsync(paymentId);
        #endregion
    }
}
