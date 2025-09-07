using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Others;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AccountPaymentsViewModel : ObservableObject, IDialogInitializer<int>
    {
        private readonly IPaymentsSummaryService _paymentsSummaryService;
        private readonly IWindowService _windowService;
        private readonly ILoadingService _loadingService;
        private int _accountId;


        public ILoadingService LoadingService => _loadingService;

        public AccountPaymentsViewModel(IPaymentsSummaryService paymentsSummaryService, IWindowService windowService, ILoadingService loadingService)
        {
            _paymentsSummaryService = paymentsSummaryService;
            _windowService = windowService;
            _loadingService = loadingService;
            PaginationInfo.PageChanged += OnPageChanged;
        }

        [ObservableProperty]
        private PaginationInfo _paginationInfo = new();

        [ObservableProperty]
        private ObservableCollection<PaymentsSummaryModel> _paymentsSummaryItems = new();

        private async Task OnPageChanged()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var pagedResult = await _paymentsSummaryService.GetPaymentsByAccountIdPagedAsync(_accountId, new PaginationParams(PaginationInfo.CurrentPage, PaginationInfo.PageSize));

                if (pagedResult != null)
                {
                    PaymentsSummaryItems = new(pagedResult.Items);
                    PaginationInfo.CurrentPage = pagedResult.PageNumber;
                    PaginationInfo.PageSize = pagedResult.PageSize;
                    PaginationInfo.TotalItems = pagedResult.TotalItems;
                    PaginationInfo.TotalPages = pagedResult.TotalPages;
                    RefreshPaginationCommandStates();
                }
            });
        }

        private void RefreshPaginationCommandStates()
        {
            FirstPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
            NextPageCommand.NotifyCanExecuteChanged();
            LastPageCommand.NotifyCanExecuteChanged();
        }

        public async Task<bool> OnOpeningDialog(int accountId)
        {
            _accountId = accountId;
            await LoadData();
            return true;
        }

        [RelayCommand(CanExecute = nameof(CanFirstPage))]
        private void FirstPage() => PaginationInfo.FirstPage();
        private bool CanFirstPage() => PaginationInfo.CanFirstPage;


        [RelayCommand(CanExecute = nameof(CanPreviousPage))]
        private void PreviousPage() => PaginationInfo.PreviousPage();
        private bool CanPreviousPage() => PaginationInfo.CanPreviousPage;


        [RelayCommand(CanExecute = nameof(CanNextPage))]
        private void NextPage() => PaginationInfo.NextPage();
        private bool CanNextPage() => PaginationInfo.CanNextPage;


        [RelayCommand(CanExecute = nameof(CanLastPage))]
        private void LastPage() => PaginationInfo.LastPage();
        private bool CanLastPage() => PaginationInfo.CanLastPage;

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }
    }
}