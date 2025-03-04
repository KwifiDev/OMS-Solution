namespace OMS.UI.Services.StatusManagement.Service
{
    public class StatusService : IStatusService
    {
        public AddEditStatus CreateAddEditStatus() => new AddEditStatus();
        public SearchStatus CreateSearchStatus() => new SearchStatus();
    }
}
