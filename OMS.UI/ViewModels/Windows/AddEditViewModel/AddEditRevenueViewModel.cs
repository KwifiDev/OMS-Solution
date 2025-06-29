using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditRevenueViewModel : AddEditBaseViewModel<RevenueModel, IRevenueService>
    {

        public AddEditRevenueViewModel(IRevenueService revenueService, IMessageService messageService,
                                       IWindowService windowService, IStatusService statusService)
                                       : base(revenueService, messageService, windowService, statusService)
        {
        }

        protected override async Task<RevenueModel?> GetByIdAsync(int revenueId)
            => await _service.GetByIdAsync(revenueId);

        protected override string GetEntityName()
            => "عائد";

        protected override async Task<bool> SaveDataAsync(bool isAdding, RevenueModel revenueModel)
            => isAdding ? await _service.AddAsync(revenueModel) : await _service.UpdateAsync(revenueModel.RevenueId, revenueModel);
    }
}
