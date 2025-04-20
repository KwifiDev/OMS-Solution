using OMS.UI.Services.StatusManagement;

namespace OMS.UI.Models.Records
{
    public record TransactionParams(int? AccountId, TransactionStatus.EnMode Mode);
}
