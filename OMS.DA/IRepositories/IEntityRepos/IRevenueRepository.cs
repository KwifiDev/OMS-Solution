namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IRevenueRepository
    {
        Task<DateOnly> GetLastAddRevenueDate();
    }
}
