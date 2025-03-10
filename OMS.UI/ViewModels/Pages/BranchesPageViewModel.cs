using AutoMapper;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class BranchesPageViewModel : BasePageViewModel<IBranchService, BranchModel>
    {
        public BranchesPageViewModel(IBranchService branchService, IMapper mapper, IDialogService dialogService,
                                     IMessageService messageService) : base(branchService, mapper, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(BranchModel item)
            => item.BranchId;

        protected override async Task LoadData()
        {
            var branchItems = await _service.GetAllAsync();
            Items = new(_mapper.Map<IEnumerable<BranchModel>>(branchItems));
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            throw new NotImplementedException();
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditBranchWindow>(itemId);
    }
}
