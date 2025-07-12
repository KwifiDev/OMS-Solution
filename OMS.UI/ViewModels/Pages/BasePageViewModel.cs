using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
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


        [ObservableProperty]
        private ObservableCollection<TModel> _items = new();

        private TModel? _selectedItem;
        protected event EventHandler? SelectedItemChanged;

        public ILoadingService LoadingService => _loadingService;

        public BasePageViewModel(TService service, TDisplayService displayService, ILoadingService loadingService,
                                 IDialogService dialogService, IMessageService messageService)
        {
            _service = service;
            _displayService = displayService;
            _loadingService = loadingService;
            _dialogService = dialogService;
            _messageService = messageService;

            SetDefaultCommandConditions();

            WeakReferenceMessenger.Default.Register<IMessage<TMessageModel>>(this, OnMessageReceived);
        }

        [RelayCommand]
        protected virtual async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var items = await _displayService.GetAllAsync();
                Items = new(items);
            });

        }

        private void SetDefaultCommandConditions()
        {
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

        private bool CanShowDetails() =>
            CommandConditions.TryGetValue(nameof(ShowDetailsCommand), out var condition) ? condition() : true;

        [RelayCommand(CanExecute = nameof(CanAddItem))]
        protected virtual async Task AddItem() => await ShowEditorWindow();

        private bool CanAddItem() =>
            CommandConditions.TryGetValue(nameof(AddItemCommand), out var condition) ? condition() : true;

        [RelayCommand(CanExecute = nameof(CanEditItem))]
        protected virtual async Task EditItem()
        {
            if (SelectedItem == null) return;
            await ShowEditorWindow(GetItemId(SelectedItem));
        }

        private bool CanEditItem() =>
            CommandConditions.TryGetValue(nameof(EditItemCommand), out var condition) ? condition() : true;

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

        private bool CanDeleteItem() =>
            CommandConditions.TryGetValue(nameof(DeleteItemCommand), out var condition) ? condition() : true;

        protected void OnSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Common Abstract Methods
        protected abstract Task<TModel> ConvertToModel(TMessageModel messageModel);
        protected abstract int GetItemId(TModel item);
        protected abstract Task<bool> ExecuteDelete(int itemId);
        protected abstract Task ShowEditorWindow(int? itemId = null);
        protected abstract Task ShowDetailsWindow(int itemId);
        #endregion
    }
}