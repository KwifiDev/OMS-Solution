namespace OMS.DA.UOW
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
