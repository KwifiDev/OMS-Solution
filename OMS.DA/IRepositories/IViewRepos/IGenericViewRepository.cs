namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IGenericViewRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
