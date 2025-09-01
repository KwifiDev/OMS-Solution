using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Extensions.Pagination;
using OMS.UI.Models.Others;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.UserSession;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public abstract partial class BasePageViewModel<TService, TDisplayService, TModel, TMessageModel> : ObservableObject
        where TModel : class
        where TMessageModel : class
        where TDisplayService : IDisplayService<TModel>
    {
        protected readonly Dictionary<string, Func<bool>> CommandConditions = new();
        protected readonly TService _service;
        protected readonly TDisplayService _displayService;
        protected readonly ILoadingService _loadingService;
        protected readonly IDialogService _dialogService;
        protected readonly IMessageService _messageService;
        protected readonly IUserSessionService _userSessionService;

        protected abstract string ViewClaim { get; }
        protected abstract string AddClaim { get; }
        protected abstract string EditClaim { get; }
        protected abstract string DeleteClaim { get; }

        [ObservableProperty]
        private PaginationInfo _paginationInfo = new();

        [ObservableProperty]
        private ObservableCollection<TModel> _items = new();

        private TModel? _selectedItem;
        protected event EventHandler? SelectedItemChanged;

        public ILoadingService LoadingService => _loadingService;

        public BasePageViewModel(TService service, TDisplayService displayService, ILoadingService loadingService,
                                 IDialogService dialogService, IMessageService messageService, IUserSessionService userSessionService)
        {
            _service = service;
            _displayService = displayService;
            _loadingService = loadingService;
            _dialogService = dialogService;
            _messageService = messageService;
            _userSessionService = userSessionService;

            PaginationInfo.PageChanged += OnPageChanged;

            SetDefaultCommandConditions();

            WeakReferenceMessenger.Default.Register<IMessage<TMessageModel>>(this, OnMessageReceived);
        }



        [RelayCommand]
        protected virtual async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var pagedResult = await _displayService.GetPagedAsync(new PaginationParams(PaginationInfo.CurrentPage, PaginationInfo.PageSize));

                if (pagedResult is not null)
                {
                    Items = new(pagedResult.Items);
                    PaginationInfo.CurrentPage = pagedResult.PageNumber;
                    PaginationInfo.PageSize = pagedResult.PageSize;
                    PaginationInfo.TotalItems = pagedResult.TotalItems;
                    PaginationInfo.TotalPages = pagedResult.TotalPages;
                }
            });

        }

        private void SetDefaultCommandConditions()
        {
            CommandConditions[nameof(AddItemCommand)] = () => true;
            CommandConditions[nameof(EditItemCommand)] = () => SelectedItem != null;
            CommandConditions[nameof(DeleteItemCommand)] = () => SelectedItem != null;
            CommandConditions[nameof(ShowDetailsCommand)] = () => SelectedItem != null;
        }

        public TModel? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    OnSelectedItemChanged();
                    RefreshCommandStates();
                }
            }
        }

        protected void RefreshCommandStates()
        {
            EditItemCommand.NotifyCanExecuteChanged();
            DeleteItemCommand.NotifyCanExecuteChanged();
            ShowDetailsCommand.NotifyCanExecuteChanged();
            AddItemCommand.NotifyCanExecuteChanged();
        }

        protected virtual async void OnMessageReceived(object recipient, IMessage<TMessageModel> message)
        {
            switch (message.Status.Operation)
            {
                case AddEditStatus.EnExecuteOperation.Added:
                    await HandleAddItem(message.Model);
                    break;
                case AddEditStatus.EnExecuteOperation.Updated:
                    await HandleEditItem(message.Model);
                    break;
            }
        }

        protected virtual async Task HandleAddItem(TMessageModel model)
        {
            Items.Add(await ConvertToModel(model));
        }

        protected virtual async Task HandleEditItem(TMessageModel model)
        {
            int index = Items.IndexOf(SelectedItem!);
            if (index >= 0) Items[index] = await ConvertToModel(model);
        }

        [RelayCommand(CanExecute = nameof(CanShowDetails))]
        protected virtual async Task ShowDetails()
        {
            if (SelectedItem == null) return;
            await ShowDetailsWindow(GetItemId(SelectedItem));
        }

        private bool CanShowDetails() => CanActionItem(nameof(ShowDetailsCommand), ViewClaim);

        [RelayCommand(CanExecute = nameof(CanAddItem))]
        protected virtual async Task AddItem() => await ShowEditorWindow();

        private bool CanAddItem() => CanActionItem(nameof(AddItemCommand), AddClaim);

        [RelayCommand(CanExecute = nameof(CanEditItem))]
        protected virtual async Task EditItem()
        {
            if (SelectedItem == null) return;
            await ShowEditorWindow(GetItemId(SelectedItem));
        }

        private bool CanEditItem() => CanActionItem(nameof(EditItemCommand), EditClaim);

        [RelayCommand(CanExecute = nameof(CanDeleteItem))]
        protected virtual async Task DeleteItem()
        {
            if (SelectedItem == null) return;

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.DeletionConfirmation))
                return;

            var success = await ExecuteDelete(GetItemId(SelectedItem));

            if (success) Items.Remove(SelectedItem);

            _messageService.ShowInfoMessage(success ? "تم الحذف" : "خطأ",
                success ? MessageTemplates.DeletionSuccessMessage : MessageTemplates.DeletionErrorMessage);
        }

        private bool CanDeleteItem() => CanActionItem(nameof(DeleteItemCommand), DeleteClaim);

        private bool CanActionItem(string nameOfItemCommand, string actionClaim)
        {
            var conditionValue = CommandConditions.TryGetValue(nameOfItemCommand, out var condition) ? condition() : true;

            return conditionValue && _userSessionService.Claims!.Contains(actionClaim);
        }

        protected void OnSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        private async Task OnPageChanged()
        {
            await LoadData();
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


        #region Common Abstract Methods
        protected abstract Task<TModel> ConvertToModel(TMessageModel messageModel);
        protected abstract int GetItemId(TModel item);
        protected abstract Task<bool> ExecuteDelete(int itemId);
        protected abstract Task ShowEditorWindow(int? itemId = null);
        protected abstract Task ShowDetailsWindow(int itemId);
        #endregion
    }
}