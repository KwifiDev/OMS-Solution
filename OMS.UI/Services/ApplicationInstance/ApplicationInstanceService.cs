using OMS.UI.Services.ApplicationInstance;

public class ApplicationInstanceService : IApplicationInstanceService, IDisposable
{
    private Mutex? _mutex;
    private const string AppMutexName = "OMS.UI";
    private bool _disposed = false;

    public bool IsFirstInstance()
    {
        try
        {
            _mutex = new Mutex(true, AppMutexName, out bool createdNew);
            return createdNew;
        }
        catch
        {
            return true;
        }
    }

    public void ReleaseMutex()
    {
        try
        {
            _mutex?.ReleaseMutex();
        }
        catch { }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            ReleaseMutex();
            _mutex?.Dispose();
            _disposed = true;
        }
    }
}