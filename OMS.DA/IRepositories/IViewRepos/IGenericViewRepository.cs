namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IGenericViewRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);
    }
}
