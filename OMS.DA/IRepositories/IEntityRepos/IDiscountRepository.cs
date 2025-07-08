using OMS.Common.Enums;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IDiscountRepository
    {
        Task<bool> IsDiscountAlreadyApplied(int serviceId, EnClientType clientType);
    }
}
