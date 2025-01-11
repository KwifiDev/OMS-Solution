using OMS.DA.Entities;
using OMS.DA.Enums;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<EnPayDebtStatus> PayAllDebtsByIdAsync(int clientId, string? notes, int createdByUserId);
    }
}
