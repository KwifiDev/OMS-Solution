namespace OMS.DA.UOW
{
    public interface IUnitOfWorkTransaction : IDisposable, IAsyncDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
