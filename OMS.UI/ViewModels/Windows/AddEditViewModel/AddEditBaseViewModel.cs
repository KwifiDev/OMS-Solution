﻿using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public abstract partial class AddEditBaseViewModel<TModel, TDto, TService> : ObservableObject, IDialogInitializer
        where TModel : class, new()
        where TDto : class
        where TService : class
    {
        protected readonly TService _service;
        protected readonly IMapper _mapper;
        protected readonly IMessageService _messageService;
        protected readonly IWindowService _windowService;

        [ObservableProperty]
        protected TModel _model = null!;

        [ObservableProperty]
        protected AddEditStatus _status;

        public AddEditBaseViewModel(TService service, IMapper mapper, IMessageService messageService,
                                    IWindowService windowService, IStatusService statusService)
        {
            _service = service;
            _mapper = mapper;
            _messageService = messageService;
            _windowService = windowService;

            Status = statusService.CreateAddEditStatus();
        }

        public virtual async Task<bool> OnOpeningDialog(int? id = -1)
        {
            try
            {
                return id > 0 ? await EnterEditModeAsync(id) : EnterAddMode();
            }
            catch (Exception ex)
            {
                ShowInitializationError(ex);
                return false;
            }
        }

        protected virtual bool EnterAddMode()
        {
            SetNewMode();
            return true;
        }

        protected virtual async Task<bool> EnterEditModeAsync(int? id)
        {
            if (id == null)
            {
                ShowError();
                return false;
            }

            var dto = await GetByIdAsync((int)id);
            if (dto == null)
            {
                ShowSearchError();
                return false;
            }

            SetEditMode(dto);

            return true;
        }

        protected virtual void SetNewMode()
        {
            Status.SelectMode = AddEditStatus.EnMode.Add;
            Model = new TModel();
        }

        protected virtual void SetEditMode(TDto dto)
        {
            Status.SelectMode = AddEditStatus.EnMode.Edit;
            Model = _mapper.Map<TModel>(dto);
        }


        [RelayCommand]
        protected virtual async Task Save(object? parameter)
        {
            if (!ValidateModel()) return;

            var dto = MapToDto();
            var isAdding = Status.SelectMode == AddEditStatus.EnMode.Add;

            bool isSuccess = await SaveDataAsync(isAdding, dto);

            if (!isSuccess)
            {
                ShowSaveError();
                return;
            }

            UpdateModelAfterSave(dto);
            UpdateStatusAndNotify(isAdding);
        }

        protected virtual bool ValidateModel()
        {
            if (Model is IValidatable validatable && !validatable.ArePropertiesValid())
            {
                ShowValidationError(validatable.GetErrors()?.FirstOrDefault()?.ErrorMessage);
                return false;
            }
            return true;
        }

        protected virtual TDto MapToDto() =>
            _mapper.Map<TDto>(Model);

        protected virtual void UpdateStatusAndNotify(bool isAdding)
        {
            Status.Operation = isAdding ?
                AddEditStatus.EnExecuteOperation.Added :
                AddEditStatus.EnExecuteOperation.Updated;

            ShowSuccessMessage(isAdding);

            SaveModelOnStatus();

            SendMessage();

        }

        protected virtual void SaveModelOnStatus() =>
            Status.ModelObject = Model;

        protected virtual void SendMessage()
        {
            var message = new ModelTransferService<TModel>
            {
                Model = Model,
                Status = Status
            };

            WeakReferenceMessenger.Default.Send<IMessage<TModel>>(message);
        }


        #region Common Abstract Methods
        // This Methods will be implemented in Derived class
        protected abstract Task<TDto?> GetByIdAsync(int id);
        protected abstract Task<bool> SaveDataAsync(bool isAdding, TDto dto);
        protected abstract void UpdateModelAfterSave(TDto dto);
        protected abstract string GetEntityName();
        #endregion

        #region Common Commands
        [RelayCommand]
        protected virtual void Close() => _windowService.Close();

        [RelayCommand]
        protected virtual void DragWindow() => _windowService.DragMove();
        #endregion

        #region Common Message Handlers
        // Common message handling methods
        protected virtual void ShowSuccessMessage(bool isAdding)
        {
            var messageType = isAdding ? MessageTemplates.AdditionSuccessMessage : MessageTemplates.UpdateSuccessMessage;
            _messageService.ShowInfoMessage($"إجراء {(isAdding ? "إضافة" : "تعديل")} {GetEntityName()}", messageType);
        }
        protected virtual void ShowInitializationError(Exception ex) =>
            _messageService.ShowErrorMessage("خطأ في التهيئة", ex.Message);

        protected virtual void ShowValidationError(string? error) =>
            _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage(error));

        protected virtual void ShowSearchError() =>
            _messageService.ShowErrorMessage("اجراء البحث عن النموذج", MessageTemplates.SearchErrorMessage);

        protected virtual void ShowError() =>
            _messageService.ShowErrorMessage("خطأ", MessageTemplates.ErrorMessage);

        protected virtual void ShowSaveError() =>
            _messageService.ShowErrorMessage("خطأ في الحفظ", MessageTemplates.SaveErrorMessage);
        #endregion

    }
}
