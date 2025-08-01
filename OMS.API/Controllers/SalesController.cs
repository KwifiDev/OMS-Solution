﻿using AutoMapper;
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
        /// POST /api/sales/
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
        /// Cancel an uncompleted sale.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// Patch /api/sales/123/cancel
        /// </remarks>
        /// <param name="saleId">The ID of the sale to be canceled.</param>
        /// <returns>Returns operation result.</returns>
        /// <response code="200">Sale canceled successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Sale not found.</response>
        /// <response code="409">Sale cannot be canceled in its current state.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("{saleId:int}/cancel")]
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
