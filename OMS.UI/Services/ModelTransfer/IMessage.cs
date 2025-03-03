
using OMS.UI.Services.StatusManagement;

namespace OMS.UI.Services.ModelTransfer
{
    public interface IMessage<TModel> where TModel : class
    {
        AddEditStatus Status { get; set; }
        TModel Model { get; set; }
    }
}
