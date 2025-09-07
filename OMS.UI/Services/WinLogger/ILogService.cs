using System.Diagnostics;

namespace OMS.UI.Services.WinLogger
{
    public interface ILogService
    {
        void Log(string message, EventLogEntryType type);
    }
}
