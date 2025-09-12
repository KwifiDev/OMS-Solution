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
    [Authorize]
    [Route("api/sales")]
    [ApiController]
    public class SalesController : GenericController<ISaleService, SaleDto, SaleModel>
    {
        public SalesController(ISaleService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     POST /api/sales
        ///     {
        ///         "clientId": 1,
        ///         "serviceId": 2,
        ///         "quantity": 1,
        ///         "description": "Sale description",
        ///         "notes": "Some notes",
        ///         "status": 0,
        ///         "createdByUserId": 5
        ///     }
        /// </remarks>
        /// <param name="dto">The data transfer object containing arguments for creating a new sale.</param>
        /// <returns>Returns the new SaleId inside the dto object.</returns>
        /// <response code="200">Returns the dto result if successful.</response>
        /// <response code="400">If the request is invalid (invalid ClientId, ServiceId, UserId, invalid amount, etc.).</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost]
        [Authorize(Policy = PermissionsData.Sales.Add)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SaleCreationDto>> AddSaleAsync([FromBody] SaleCreationDto dto)
        {
            try
            {
                var model = _mapper.Map<SaleCreationModel>(dto);

                bool isSuccess = await _service.AddSaleAsync(model);
                dto.SaleId = model.SaleId;
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



        /// <summary>
        /// Cancels an uncompleted sale.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     PATCH /api/sales/123/cancel
        /// </remarks>
        /// <param name="saleId">The ID of the sale to be canceled.</param>
        /// <returns>Returns the operation result.</returns>
        /// <response code="200">Sale canceled successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Sale not found.</response>
        /// <response code="409">Sale cannot be canceled in its current state.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("{saleId:int}/cancel")]
        [Authorize(Policy = PermissionsData.Sales.Cancel)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelSaleAsync([FromRoute] int saleId)
        {
            if (saleId <= 0) return BadRequest("Invalid sale ID");

            try
            {
                var isSuccess = await _service.CancelSaleAsync(saleId);

                if (isSuccess is null) return NotFound();

                if (isSuccess == false) return Conflict("Sale Status may be already completed or canceled");

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Server Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }




        [NonAction] // This EndPoint Insted Of (AddSaleAsync) Method
        public override Task<ActionResult<SaleDto>> AddAsync([FromBody] SaleDto dto)
            => Task.FromResult<ActionResult<SaleDto>>(NotFound("This endpoint is disabled."));

        #region override abstract Methods
        protected override async Task<bool> AddModelAsync(SaleModel model) => await _service.AddAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override async Task<PagedResult<SaleModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<SaleModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override int GetModelId(SaleModel model) => model.Id;
        protected override bool IsIdentifierIdentical(int id, SaleDto dto) => dto.Id == id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override void SetDtoId(SaleDto dto, int id) => dto.Id = id;
        protected override async Task<bool> UpdateModelAsync(SaleModel model) => await _service.UpdateAsync(model);
        #endregion
    }
}
