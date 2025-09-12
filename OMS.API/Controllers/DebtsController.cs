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
    [Route("api/debts")]
    [ApiController]
    public class DebtsController : GenericController<IDebtService, DebtDto, DebtModel>
    {
        public DebtsController(IDebtService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// Creates a new Debt.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// POST /api/debts
        /// </remarks>
        /// <param name="dto">The data transfer object containing arguments for creating a new Debt.</param>
        /// <returns>Returns the new DebtId inside the dto object.</returns>
        /// <response code="200">Returns the dto result if successful.</response>
        /// <response code="400">If the request is invalid (invalid ClientId, ServiceId, UserId, invalid amount, etc.).</response>
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
        /// Cancels a not paid Debt.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// PATCH /api/debts/123/cancel
        /// </remarks>
        /// <param name="debtId">The ID of the Debt to be canceled.</param>
        /// <returns>Returns the operation result.</returns>
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
        /// Pays a Debt.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// POST /api/debts/pay
        /// </remarks>
        /// <param name="payDebtDto">The data transfer object containing payment details.</param>
        /// <returns>Returns the operation result.</returns>
        /// <response code="200">Debt paid successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Debt not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("pay")]
        [Authorize(Policy = PermissionsData.Debts.Pay)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> PayDebtAsync([FromBody] PayDebtDto payDebtDto)
        {
            if (payDebtDto.DebtId <= 0) return NotFound();

            try
            {
                var isExist = await _service.IsExistAsync(payDebtDto.DebtId);
                if (!isExist) return NotFound();

                var payDebtModel = _mapper.Map<PayDebtModel>(payDebtDto);

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


        /// <summary>
        /// Calculates the total debts for a specific client.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// GET /api/debts/calctotaldebts/123/totaldebts
        /// 123 is the clientId.
        /// </remarks>
        /// <param name="clientId">The client ID to calculate total debts for (must be a positive integer).</param>
        /// <returns>The total debts for the specified client.</returns>
        /// <response code="200">Returns the total debts.</response>
        /// <response code="404">If the client or debts were not found.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet("calctotaldebts/{clientId:int}/totaldebts")]
        [Authorize(Policy = PermissionsData.Clients.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<decimal>> CalcTotalDebtsByClientIdAsync([FromRoute] int clientId)
        {
            if (clientId <= 0) return NotFound();

            try
            {
                var totalDebts = await _service.CalcTotalDebtsByClientIdAsync(clientId);
                return totalDebts is null ? NotFound() : Ok(totalDebts);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving clientId",
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
        protected override int GetModelId(DebtModel model) => model.Id;
        protected override bool IsIdentifierIdentical(int id, DebtDto dto) => dto.Id == id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override void SetDtoId(DebtDto dto, int id) => dto.Id = id;
        protected override async Task<bool> UpdateModelAsync(DebtModel model) => await _service.UpdateAsync(model);
        #endregion
    }
}
