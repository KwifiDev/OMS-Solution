using OMS.DA.Entities;
using OMS.DA.Enums;


namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IDebtRepository : IGenericRepository<Debt>
    {
        Task<EnPayDebtStatus> PayDebtByIdAsync(int debtId, string? notes, int createdByUserId);
    }
}
