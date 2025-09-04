using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class DiscountsAppliedViewModel : BasePageViewModel<IDiscountService, IDiscountsAppliedService, DiscountsAppliedModel, DiscountModel>, IDialogInitializer<int>
    {
        protected override string ViewClaim => PermissionsData.DiscountsApplied.View;
        protected override string AddClaim => PermissionsData.Discounts.Add;
        protected override string EditClaim => PermissionsData.Discounts.Edit;
        protected override string DeleteClaim => PermissionsData.Discounts.Delete;

        private readonly IWindowService _windowService;
        private int _serviceId;

        //[ObservableProperty]
        //private PaginationInfo _paginationInfo = new();

        public DiscountsAppliedViewModel(IDiscountService service, IDiscountsAppliedService displayService, ILoadingService loadingService,
                                         IDialogService dialogService, IMessageService messageService, IWindowService windowService, IUserSessionService userSessionService)
                                         : base(service, displayService, loadingService, dialogService, messageService, userSessionService)
        {
            _windowService = windowService;
            //PaginationInfo.PageChanged += OnPageChanged;
        }

        //private async Task OnPageChanged()
        //{
        //    await LoadData();
        //}

        public async Task<bool> OnOpeningDialog(int serviceId)
        {
            if (serviceId <= 0) return false;

            _serviceId = serviceId;
            await LoadData();

            return true;
        }

        protected override async Task<DiscountsAppliedModel> ConvertToModel(DiscountModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.Id))!;

        protected override async Task<bool> ExecuteDelete(int itemId) => await _service.DeleteAsync(itemId);

        protected override int GetItemId(DiscountsAppliedModel item) => item.Id;

        protected override async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var pagedResultDiscountsApplied = await _displayService.GetDiscountsByServiceIdPagedAsync(_serviceId, new PaginationParams(PaginationInfo.CurrentPage, PaginationInfo.PageSize));

                if (pagedResultDiscountsApplied != null)
                {
                    Items = new(pagedResultDiscountsApplied.Items);
                    PaginationInfo.CurrentPage = pagedResultDiscountsApplied.PageNumber;
                    PaginationInfo.PageSize = pagedResultDiscountsApplied.PageSize;
                    PaginationInfo.TotalItems = pagedResultDiscountsApplied.TotalItems;
                    PaginationInfo.TotalPages = pagedResultDiscountsApplied.TotalPages;
                    RefreshPaginationCommandStates();
                }
            });
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditDiscountWindow, (int? DiscountId, int ServiceId)>((itemId, _serviceId));

        [RelayCommand]
        private void Close() => _windowService.Close();


        // This Method is Disabled
        protected override Task ShowDetailsWindow(int itemId) => Task.CompletedTask;
    }
}