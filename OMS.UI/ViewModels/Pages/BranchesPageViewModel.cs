using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Views.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public partial class BranchesPageViewModel : ObservableObject
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;
        private readonly IMessageService _messageService;

        [ObservableProperty]
        private ObservableCollection<BranchModel> _branches = null!;

        [ObservableProperty]
        private BranchModel? _selectedBranch;

        public BranchesPageViewModel(IBranchService branchService, IMapper mapper, IDialogService dialogService, IMessageService messageService)
        {
            _branchService = branchService;
            _mapper = mapper;
            _dialogService = dialogService;
            _messageService = messageService;

            WeakReferenceMessenger.Default.Register<IMessage<BranchModel>>(this, OnMessageReceived);

        }

        private void OnMessageReceived(object recipient, IMessage<BranchModel> message)
        {
            switch (message.Status.Operation)
            {
                case AddEditStatus.EnExecuteOperation.Added:
                    OnBranchAdd(message.Model);
                    break;
                case AddEditStatus.EnExecuteOperation.Updated:
                    OnBranchEdit(message.Model);
                    break;
                default:
                    // Handle other cases if needed
                    break;
            }
        }

        private void OnBranchAdd(BranchModel? branch)
        {
            if (branch == null) return;
            BranchModel? branchModel = branch as BranchModel;

            Branches!.Add(branchModel!);
        }

        private void OnBranchEdit(BranchModel? branch)
        {
            if (branch == null) return;
            BranchModel? branchModel = branch as BranchModel;

            int branchIndex = Branches!.IndexOf(SelectedBranch!);
            Branches[branchIndex] = branchModel!;
        }


        [RelayCommand]
        private async Task LoadData()
        {
            var branchesDto = await _branchService.GetAllAsync();
            var branches = _mapper.Map<IEnumerable<BranchModel>>(branchesDto);
            Branches = new ObservableCollection<BranchModel>(branches ?? Enumerable.Empty<BranchModel>());
        }

        [RelayCommand]
        private async Task AddBranch()
        {
            await _dialogService.ShowDialog<AddEditBranchWindow>();
        }

        [RelayCommand]
        private async Task EditBranch(BranchModel? branch)
        {
            if (branch == null) return;
            SelectItem(branch);

            await _dialogService.ShowDialog<AddEditBranchWindow>(branch.BranchId);
        }

        [RelayCommand]
        private async Task DeleteBranch(BranchModel? branch)
        {
            if (branch == null) return;
            SelectItem(branch);

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.DeletionConfirmation))
                return;

            bool isDeleted = await _branchService.DeleteAsync(branch.BranchId);
            if (isDeleted)
            {
                Branches!.Remove(SelectedBranch!);
                _messageService.ShowInfoMessage("اجراء حذف", MessageTemplates.DeletionSuccessMessage);
            }
            else
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.DeletionErrorMessage);
            }
        }

        private void SelectItem(BranchModel branch)
        {
            SelectedBranch = branch;
        }

    }
}
