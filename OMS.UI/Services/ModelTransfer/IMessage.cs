
using OMS.UI.Services.Status;

namespace OMS.UI.Services.ModelTransfer
{
    public interface IMessage<TModel> where TModel : class
    {
        StatusInfo Status { get; set; }
        TModel Model { get; set; }
    }
}
