using Microsoft.Extensions.DependencyInjection;
using OMS.UI.Configuration;
using OMS.UI.Views;
using System.Windows;

namespace OMS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            Bootstrapper bootstrapper = new Bootstrapper();
            ServiceProvider = bootstrapper.ConfigureServices();

            Window mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        }
    }

}
