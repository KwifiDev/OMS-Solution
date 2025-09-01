using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Others;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AccountTransactionsViewModel : ObservableObject, IDialogInitializer<int>
    {
        private readonly ITransactionsSummaryService _transactionsSummaryService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private PaginationInfo _paginationInfo = new();

        public AccountTransactionsViewModel(ITransactionsSummaryService transactionsSummaryService, IWindowService windowService)
        {
            _transactionsSummaryService = transactionsSummaryService;
            _windowService = windowService;
            PaginationInfo.PageChanged += OnPageChanged;
        }




        [ObservableProperty]
        private ObservableCollection<TransactionsSummaryModel> _transactionsSummaryItems = new();
        private int _accountId;

        private async Task OnPageChanged()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var pagedResult = await _transactionsSummaryService.GetTransactionsByAccountIdPagedAsync(_accountId, new PaginationParams(PaginationInfo.CurrentPage, PaginationInfo.PageSize));

            if (pagedResult is not null)
            {
                TransactionsSummaryItems = new(pagedResult.Items);

                PaginationInfo.CurrentPage = pagedResult.PageNumber;
                PaginationInfo.PageSize = pagedResult.PageSize;
                PaginationInfo.TotalItems = pagedResult.TotalItems;
                PaginationInfo.TotalPages = pagedResult.TotalPages;
            }
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