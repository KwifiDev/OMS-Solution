using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.Common.Data;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    [Authorize]
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
        [Authorize(Policy = PermissionsData.PaymentsSummary.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<PaymentsSummaryDto>>> GetPaymentsByAccountIdPagedAsync([FromRoute] int accountId, [FromQuery] PaginationParams parameters)
        {
            try
            {
                var pagedResult = await _service.GetPaymentsByAccountIdPagedAsync(accountId, parameters);
                return Ok(new PagedResult<PaymentsSummaryDto>
                {
                    Items = _mapper.Map<List<PaymentsSummaryDto>>(pagedResult.Items),
                    TotalCount = pagedResult.TotalCount,
                    PageNumber = pagedResult.PageNumber,
                    PageSize = pagedResult.PageSize
                });
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
        protected override async Task<PagedResult<PaymentsSummaryModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<PaymentsSummaryModel?> GetModelByIdAsync(int paymentId) => await _service.GetByIdAsync(paymentId);
        #endregion
    }
}
