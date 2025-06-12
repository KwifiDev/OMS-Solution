using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditSaleViewModel : AddEditBaseViewModel<SaleModel, ISaleService>, IDialogInitializer<(int? SaleId, int ClientId)>

    {
        private readonly IServiceService _serviceService;
        private readonly IUserSessionService _userSessionService;

        [ObservableProperty]
        private ObservableCollection<ServiceOptionModel> _services = null!;

        [ObservableProperty]
        private ObservableCollection<SaleStatus> _salesStatus = null!;

        public AddEditSaleViewModel(IServiceService serviceService, ISaleService service, IMessageService messageService, IWindowService windowService,
                                    IStatusService statusService, IUserSessionService userSessionService) : base(service, messageService, windowService, statusService)
        {
            _serviceService = serviceService;
            _userSessionService = userSessionService;
            InitializeSaleStatus();
            InitializeServices();
        }


        public async Task<bool> OnOpeningDialog((int? SaleId, int ClientId) parameters)
        {
            var isSuccess = await base.OnOpeningDialog(parameters.SaleId);
            Model.ClientId = parameters.ClientId;
            Model.CreatedByUserId = _userSessionService.CurrentUser!.UserId;
            return isSuccess;
        }

        public record SaleStatus(string DisplayMember, EnSaleStatus Value);

        private void InitializeSaleStatus()
        {
            SalesStatus =
            [
                 new("غير مكتمل", EnSaleStatus.Uncompleted),
                 new("مكتمل", EnSaleStatus.Completed)
            ];
        }

        private async void InitializeServices()
        {
            var serviceOption = await _serviceService.GetAllServicesOption();
            Services = new(serviceOption);
        }

        protected override async Task<SaleModel?> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

        protected override string GetEntityName() => "مبيعة";

        protected override async Task<bool> SaveDataAsync(bool isAdding, SaleModel model)
        {
            if (isAdding)
            {
                var createSaleModel = new CreateSaleModel
                {
                    ServiceId = model.ServiceId,
                    ClientId = model.ClientId,
                    Quantity = model.Quantity,
                    Description = model.Description,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedByUserId = model.CreatedByUserId
                };

                bool isSuccess = await _service.AddSaleAsync(createSaleModel);

                if (!isSuccess) return false;

                return (model.SaleId = createSaleModel.SaleId) > 0;
            }
            else
            {
                return await _service.UpdateAsync(model.SaleId, model);
            }

        }

    }
}