namespace OMS.UI.Services.StatusManagement.Service
{
    public class StatusService : IStatusService
    {
        public AddEditStatus CreateAddEditStatus() => new AddEditStatus();
        public SearchStatus CreateSearchStatus() => new SearchStatus();
        public TransactionStatus CreateTransactionStatus() => new TransactionStatus();
        public DebtStatus CreateDebtStatus() => new DebtStatus();
    }
}
