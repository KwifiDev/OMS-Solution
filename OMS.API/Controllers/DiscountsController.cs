using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountsController : GenericController<IDiscountService, DiscountDto, DiscountModel>
    {
        public DiscountsController(IDiscountService service, IMapper mapper) : base(service, mapper)
        {
        }



        #region override abstract Methods
        protected override async Task<bool> AddModelAsync(DiscountModel model) => await _service.AddAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override async Task<IEnumerable<DiscountModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<DiscountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override int GetModelId(DiscountModel model) => model.DiscountId;
        protected override bool IsIdentifierIdentical(int id, DiscountDto dto) => dto.DiscountId == id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override void SetDtoId(DiscountDto dto, int id) => dto.DiscountId = id;
        protected override async Task<bool> UpdateModelAsync(DiscountModel model) => await _service.UpdateAsync(model);
        #endregion
    }
}
