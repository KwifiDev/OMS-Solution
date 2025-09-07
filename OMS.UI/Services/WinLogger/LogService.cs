using System.Diagnostics;
using System.Windows;

namespace OMS.UI.Services.WinLogger
{
    public class LogService : ILogService
    {
        private const string Source = "OMS.UI";
        private const string LogName = "OMS.Log";

        public void Log(string message, EventLogEntryType type)
        {
            try
            {
                if (!EventLog.SourceExists(Source))
                {
                    EventLog.CreateEventSource(new EventSourceCreationData(Source, LogName));
                }

                using EventLog eventLog = new(LogName);
                eventLog.Source = Source;
                eventLog.WriteEntry(message, type);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
