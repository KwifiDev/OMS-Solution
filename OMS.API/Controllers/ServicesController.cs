using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : GenericController<IServiceService, ServiceDto, ServiceModel>
    {
        public ServicesController(IServiceService service, IMapper mapper) : base(service, mapper)
        {
        }


        #region override abstract Methods
        protected override async Task<IEnumerable<ServiceModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<ServiceModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(ServiceModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(ServiceModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override int GetModelId(ServiceModel model) => model.ServiceId;
        protected override bool IsIdentifierIdentical(int id, ServiceDto dto) => dto.ServiceId == id;
        protected override void SetDtoId(ServiceDto dto, int id) => dto.ServiceId = id;
        #endregion
    }
}
