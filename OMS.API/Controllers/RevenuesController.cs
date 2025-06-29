using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing revenues.
    /// </summary>
    [Route("api/revenues")]
    [ApiController]
    public class RevenuesController : GenericController<IRevenueService, RevenueDto, RevenueModel>
    {
        /// <summary>
        /// Initializes a new instance of the AccountsController class.
        /// </summary>
        /// <param name="accountService">The account service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public RevenuesController(IRevenueService accountService, IMapper mapper) : base(accountService, mapper)
        {
        }


        #region override abstract Methods
        protected override int GetModelId(RevenueModel model) => model.RevenueId;
        protected override void SetDtoId(RevenueDto dto, int id) => dto.RevenueId = id;
        protected override async Task<IEnumerable<RevenueModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<RevenueModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(RevenueModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(RevenueModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, RevenueDto dto) => id == dto.RevenueId;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
