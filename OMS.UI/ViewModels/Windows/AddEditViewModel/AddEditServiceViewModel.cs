using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public class AddEditServiceViewModel : AddEditBaseViewModel<ServiceModel, IServiceService>
    {
        public AddEditServiceViewModel(IServiceService service, IMessageService messageService,
                                       IWindowService windowService, IStatusService statusService)
                                       : base(service, messageService, windowService, statusService)
        {
        }

        protected override async Task<ServiceModel?> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

        protected override string GetEntityName() => "خدمة";

        protected override async Task<bool> SaveDataAsync(bool isAdding, ServiceModel serviceModel)
            => isAdding ? await _service.AddAsync(serviceModel) : await _service.UpdateAsync(serviceModel.Id, serviceModel);
    }
}