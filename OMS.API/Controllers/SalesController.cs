using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.StoredProcedureParams;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : GenericController<ISaleService, SaleDto, SaleModel>
    {
        public SalesController(ISaleService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// create a new sale.
        /// </summary>
        /// <remarks>
        /// POST /api/sales/add
        /// </remarks>
        /// <param name="dto">The data transfer object containing sale args for creating new sale.</param>
        /// <returns>Returns the new saleId inside dto object of the sale.</returns>
        /// <response code="200">Returns the dto result if successful.</response>
        /// <response code="400">If the request is invalid (invalid clientID, serviceID, UserID, invalid amount, etc.).</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateSaleDto>> AddSaleAsync([FromBody] CreateSaleDto dto)
        {
            try
            {
                var model = _mapper.Map<CreateSaleModel>(dto);

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
        /// cancel a uncomplted sale.
        /// </summary>
        /// <remarks>
        /// POST /api/sales/cancel/123
        /// </remarks>
        /// <param name="saleId">The sale ID containing sale canceling uncompleted sale.</param>
        /// <returns>Returns true if the sale was canceled other wise return false.</returns>
        /// <response code="200">Returns the boolean result.</response>
        /// <response code="400">If the request is invalid (invalid sale ID).</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPut("cancel/{saleId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CancelSaleAsync([FromRoute] int saleId)
        {
            try
            {
                bool isSuccess = await _service.CancelSaleAsync(saleId);
                return Ok(isSuccess);
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
        protected override async Task<IEnumerable<SaleModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<SaleModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override int GetModelId(SaleModel model) => model.SaleId;
        protected override bool IsIdentifierIdentical(int id, SaleDto dto) => dto.SaleId == id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override void SetDtoId(SaleDto dto, int id) => dto.SaleId = id;
        protected override async Task<bool> UpdateModelAsync(SaleModel model) => await _service.UpdateAsync(model);
        #endregion
    }
}
