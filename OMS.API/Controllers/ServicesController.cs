using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Dtos.Tables;
using OMS.Common.Dtos.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/services")]
    [ApiController]
    public class ServicesController : GenericController<IServiceService, ServiceDto, ServiceModel>
    {
        public ServicesController(IServiceService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// Retrieves all Services option.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/services/option
        ///     
        /// Returns all available Services option in the system.
        /// </remarks>
        /// <returns>List of all Services option</returns>
        /// <response code="200">Returns the complete list of Services option</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("options")]
        [Authorize(Policy = PermissionsData.Services.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceOptionDto>>> GetAllServicesOptionAsync()
        {
            try
            {
                var models = await _service.GetAllServicesOption();
                return Ok(_mapper.Map<IEnumerable<ServiceOptionDto>>(models));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving Services option",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        #region override abstract Methods
        protected override async Task<PagedResult<ServiceModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<ServiceModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(ServiceModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(ServiceModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override int GetModelId(ServiceModel model) => model.Id;
        protected override bool IsIdentifierIdentical(int id, ServiceDto dto) => dto.Id == id;
        protected override void SetDtoId(ServiceDto dto, int id) => dto.Id = id;
        #endregion
    }
}
