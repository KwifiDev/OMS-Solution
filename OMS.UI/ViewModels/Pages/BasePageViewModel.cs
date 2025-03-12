using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public abstract partial class BasePageViewModel<TService, TDisplayService, TModel, TMessageModel> : ObservableObject
        where TModel : class
        where TMessageModel : class
    {
        protected readonly TService _service;
        protected readonly TDisplayService _displayService;
        protected readonly IMapper _mapper;
        protected readonly IDialogService _dialogService;
        protected readonly IMessageService _messageService;

        [ObservableProperty]
        private ObservableCollection<TModel> _items = new();

        [ObservableProperty]
        private TModel? _selectedItem;

        public BasePageViewModel(TService service, TDisplayService displayService, IMapper mapper, IDialogService dialogService, IMessageService messageService)
        {
            _service = service;
            _displayService = displayService;
            _mapper = mapper;
            _dialogService = dialogService;
            _messageService = messageService;

            WeakReferenceMessenger.Default.Register<IMessage<TMessageModel>>(this, OnMessageReceived);
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


        [RelayCommand]
        protected virtual async Task ShowDetails()
        {
            if (SelectedItem == null) return;
            await ShowDetailsWindow(GetItemId(SelectedItem));
        }

        [RelayCommand]
        protected virtual async Task AddItem()
            => await ShowEditorWindow();

        [RelayCommand]
        protected virtual async Task EditItem()
        {
            if (SelectedItem == null) return;
            await ShowEditorWindow(GetItemId(SelectedItem));
        }

        [RelayCommand]
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


        #region Common Abstract Methods
        // This Methods will be implemented in Derived class

        [RelayCommand]
        protected abstract Task LoadData();

        protected abstract Task<TModel> ConvertToModel(TMessageModel messageModel);
        protected abstract int GetItemId(TModel item);
        protected abstract Task<bool> ExecuteDelete(int itemId);
        protected abstract Task ShowEditorWindow(int? itemId = null);
        protected abstract Task ShowDetailsWindow(int itemId);
        #endregion
    }
}

