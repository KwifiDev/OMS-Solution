using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditDiscountViewModel : AddEditBaseViewModel<DiscountModel, IDiscountService>, IDialogInitializer<(int? DiscountId, int ServiceId)>
    {

        [ObservableProperty]
        private ObservableCollection<ClientType> _clientTypes = null!;

        public AddEditDiscountViewModel(IDiscountService service, IMessageService messageService, IWindowService windowService,
                                        IStatusService statusService) : base(service, messageService, windowService, statusService)
        {
            InitializeClientTypes();
        }

        public record ClientType(string DisplayMember, EnClientType Value);

        public async Task<bool> OnOpeningDialog((int? DiscountId, int ServiceId) parameters)
        {
            var isSuccess = await base.OnOpeningDialog(parameters.DiscountId);
            Model.ServiceId = parameters.ServiceId;
            return isSuccess;
        }

        protected override async Task<DiscountModel?> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

        protected override string GetEntityName() => "خصم الخدمة";

        protected override async Task<bool> SaveDataAsync(bool isAdding, DiscountModel model)
            => isAdding ? await _service.AddAsync(model) : await _service.UpdateAsync(model.Id, model);

        private void InitializeClientTypes()
        {
            ClientTypes =
            [
                new("عادي", EnClientType.Normal),
                new("محامي", EnClientType.Lawyer),
                new("اخر", EnClientType.Other)
            ];
        }


    }
}