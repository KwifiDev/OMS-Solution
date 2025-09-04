using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditDebtViewModel : AddEditBaseViewModel<DebtModel, IDebtService>, IDialogInitializer<(int? DebtId, int ClientId)>

    {
        private readonly IServiceService _serviceService;
        private readonly IUserSessionService _userSessionService;

        [ObservableProperty]
        private ObservableCollection<ServiceOptionModel> _services = null!;


        public AddEditDebtViewModel(IServiceService serviceService, IDebtService service, IMessageService messageService, IWindowService windowService,
                                    IStatusService statusService, IUserSessionService userSessionService) : base(service, messageService, windowService, statusService)
        {
            _serviceService = serviceService;
            _userSessionService = userSessionService;
            
        }


        public async Task<bool> OnOpeningDialog((int? DebtId, int ClientId) parameters)
        {
            var isSuccess = await base.OnOpeningDialog(parameters.DebtId);
            Model.ClientId = parameters.ClientId;
            Model.CreatedByUserId = _userSessionService.CurrentUser!.Id;
            var isServicesLoaded = await InitializeServices();
            return isSuccess && isServicesLoaded;
        }


        private async Task<bool> InitializeServices()
        {
            var serviceOption = await _serviceService.GetAllServicesOption();
            Services = new(serviceOption);
            return Services.Count > 0;
        }

        protected override async Task<DebtModel?> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

        protected override string GetEntityName() => "مبيعة";

        protected override async Task<bool> SaveDataAsync(bool isAdding, DebtModel model)
        {
            if (isAdding)
            {
                var createDebtModel = new DebtCreationModel
                {
                    ServiceId = model.ServiceId,
                    ClientId = model.ClientId,
                    Quantity = model.Quantity,
                    Description = model.Description,
                    Notes = model.Notes,
                    CreatedByUserId = model.CreatedByUserId
                };

                bool isSuccess = await _service.AddDebtAsync(createDebtModel);

                if (!isSuccess) return false;

                return (model.Id = createDebtModel.Id) > 0;
            }
            else
            {
                return await _service.UpdateAsync(model.Id, model);
            }

        }

    }
}