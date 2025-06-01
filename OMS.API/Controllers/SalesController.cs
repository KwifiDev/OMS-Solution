using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
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
