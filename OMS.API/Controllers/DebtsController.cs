using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.StoredProcedureParams;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/debts")]
    [ApiController]
    public class DebtsController : GenericController<IDebtService, DebtDto, DebtModel>
    {
        public DebtsController(IDebtService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// create a new Debt.
        /// </summary>
        /// <remarks>
        /// POST /api/Debts/
        /// </remarks>
        /// <param name="dto">The data transfer object containing Debt args for creating new Debt.</param>
        /// <returns>Returns the new debtId inside dto object of the Debt.</returns>
        /// <response code="200">Returns the dto result if successful.</response>
        /// <response code="400">If the request is invalid (invalid clientID, serviceID, UserID, invalid amount, etc.).</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost]
        [Authorize(Policy = PermissionsData.Debts.Add)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DebtCreationDto>> AddDebtAsync([FromBody] DebtCreationDto dto)
        {
            try
            {
                var model = _mapper.Map<DebtCreationModel>(dto);

                bool isSuccess = await _service.AddDebtAsync(model);
                dto.DebtId = model.DebtId;
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
        /// Cancel an Not Paid Debt.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// Patch /api/Debts/123/cancel
        /// </remarks>
        /// <param name="debtId">The ID of the Debt to be canceled.</param>
        /// <returns>Returns operation result.</returns>
        /// <response code="200">Debt canceled successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Debt not found.</response>
        /// <response code="409">Debt cannot be canceled in its current state.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("{debtId:int}/cancel")]
        [Authorize(Policy = PermissionsData.Debts.Cancel)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelDebtAsync([FromRoute] int debtId)
        {
            if (debtId <= 0) return BadRequest("Invalid Debt ID");

            try
            {
                var isSuccess = await _service.CancelDebtAsync(debtId);

                if (isSuccess is null) return NotFound();

                if (isSuccess == false) return Conflict("Debt Status may be already paid or canceled");

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


        /// <summary>
        /// Pay a Debt.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// Patch /api/debts/pay
        /// </remarks>
        /// <param name="PayDebtDto">The data transfare object.</param>
        /// <returns>Returns operation result.</returns>
        /// <response code="200">Debt paid successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Debt not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("pay")]
        [Authorize(Policy = PermissionsData.Debts.Pay)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> PayDebtAsync([FromBody] PayDebtDto PayDebtDto)
        {
            try
            {
                var isExist = await _service.IsExistAsync(PayDebtDto.DebtId);
                if (!isExist) return NotFound();

                var payDebtModel = _mapper.Map<PayDebtModel>(PayDebtDto);

                await _service.PayDebtByIdAsync(payDebtModel);

                return Ok((int)payDebtModel.PayDebtStatus);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Server Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        [NonAction] // This EndPoint Insted Of (AddDebtAsync) Method
        public override Task<ActionResult<DebtDto>> AddAsync([FromBody] DebtDto dto)
            => Task.FromResult<ActionResult<DebtDto>>(NotFound("This endpoint is disabled."));

        #region override abstract Methods
        protected override async Task<bool> AddModelAsync(DebtModel model) => await _service.AddAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override async Task<PagedResult<DebtModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<DebtModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override int GetModelId(DebtModel model) => model.DebtId;
        protected override bool IsIdentifierIdentical(int id, DebtDto dto) => dto.DebtId == id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override void SetDtoId(DebtDto dto, int id) => dto.DebtId = id;
        protected override async Task<bool> UpdateModelAsync(DebtModel model) => await _service.UpdateAsync(model);
        #endregion
    }
}
