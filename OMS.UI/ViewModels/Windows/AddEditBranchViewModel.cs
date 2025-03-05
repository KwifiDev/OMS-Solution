using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AddEditBranchViewModel : ObservableObject, IDialogInitializer
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private BranchModel _branch = null!;

        [ObservableProperty]
        private AddEditStatus _status;

        public AddEditBranchViewModel(IBranchService branchService, IMapper mapper, IMessageService messageService,
                                      IWindowService windowService, IStatusService statusService)
        {
            _branchService = branchService;
            _mapper = mapper;
            _messageService = messageService;
            _windowService = windowService;
            Status = statusService.CreateAddEditStatus();
        }

        public async Task<bool> Initialize(int? personId = -1)
        {
            try
            {
                return personId > 0 ? await EnterEditModeAsync(personId) : EnterAddMode();
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage("خطأ في التهيئة", ex.Message);
                return false;
            }
        }

        private bool EnterAddMode()
        {
            Status.SelectMode = AddEditStatus.EnMode.Add;

            Branch = new BranchModel();
            return true;
        }

        private async Task<bool> EnterEditModeAsync(int? branchId)
        {
            if (branchId == null)
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.ErrorMessage);
                return false;
            }

            var branchDto = await _branchService.GetByIdAsync((int)branchId);
            if (branchDto == null)
            {
                _messageService.ShowErrorMessage("اجراء البحث عن شخص", MessageTemplates.SearchErrorMessage);
                return false;
            }

            Status.SelectMode = AddEditStatus.EnMode.Edit;

            Branch = _mapper.Map<BranchModel>(branchDto);
            return true;
        }

        private bool ValidateBranch()
        {
            if (!Branch.ArePropertiesValid())
            {
                _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage(Branch.GetErrors()?.FirstOrDefault()?.ErrorMessage));
                return false;
            }
            return true;
        }

        private BranchDto MapBranchToDto()
        {
            return _mapper.Map<BranchDto>(Branch);
        }

        private async Task<bool> SavePersonData(bool isAdding, BranchDto branchDto)
        {
            return isAdding
                ? await _branchService.AddAsync(branchDto)
                : await _branchService.UpdateAsync(branchDto);
        }

        private void UpdateStatusAndNotifyBranch(bool isAdding, BranchDto branchDto)
        {
            if (isAdding)
            {
                Branch.BranchId = branchDto.BranchId;
                Status.Operation = AddEditStatus.EnExecuteOperation.Added;

                _messageService.ShowInfoMessage("اجراء اضافة فرع جديد", MessageTemplates.AdditionSuccessMessage);
            }
            else
            {
                Status.Operation = AddEditStatus.EnExecuteOperation.Updated;

                _messageService.ShowInfoMessage("اجراء تعديل بيانات فرع", MessageTemplates.UpdateSuccessMessage);
            }

            SendMessage();

        }

        private void SendMessage()
        {
            var message = new ModelTransferService<BranchModel> { Model = Branch, Status = Status };
            WeakReferenceMessenger.Default.Send<IMessage<BranchModel>>(message);
        }


        [RelayCommand]
        private async Task SaveBranch(object? parameter)
        {
            if (!ValidateBranch()) return;

            var branchDto = MapBranchToDto();
            var isAdding = Status.SelectMode == AddEditStatus.EnMode.Add;

            bool isSuccess = await SavePersonData(isAdding, branchDto);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("اجراء حفظ بيانات شخص", MessageTemplates.SaveErrorMessage);
                return;
            }

            UpdateStatusAndNotifyBranch(isAdding, branchDto);
            Status.IsModifiable = false;
            Status.ModelObject = Branch;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }

        [RelayCommand]
        private void DragWindow()
        {
            _windowService.DragMove();
        }
    }
}
