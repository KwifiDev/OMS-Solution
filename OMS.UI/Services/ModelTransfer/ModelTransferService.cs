
using OMS.UI.Services.StatusManagement;

namespace OMS.UI.Services.ModelTransfer
{
    public class ModelTransferService<TModel> : IMessage<TModel> where TModel : class
    {
        private WeakReference<AddEditStatus> _status = null!;
        private WeakReference<TModel> _model = null!;

        public required AddEditStatus Status
        {
            get => _status.TryGetTarget(out var status) ? status : throw new InvalidOperationException("Status reference is null.");
            set => _status = new WeakReference<AddEditStatus>(value);
        }

        public required TModel Model
        {
            get => _model.TryGetTarget(out var model) ? model : throw new InvalidOperationException("Model reference is null.");
            set => _model = new WeakReference<TModel>(value);
        }

        public bool Validate()
        {
            return Status != null && Model != null;
        }
    }
}
