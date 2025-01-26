using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMS.BL.Mapping;
using OMS.DA.Context;
using OMS.UI.Views;
using System.Windows;

namespace OMS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host = CreateHostBuilder(e.Args).Build();
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    RegisterDbContext(services, context.Configuration);
                    RegisterRepositories(services);
                    RegisterServices(services);
                    RegisterMapper(services);
                    RegisterViewModels(services);

                    // Register main window
                    services.AddTransient<MainWindow>();
                });

        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Register other repositories
        }

        private static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericService<>), typeof(GenericService<,>));
            // Register other services
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IMapperService, MapperService>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            //services.AddTransient<MainWindowViewModel>();
            // Register other ViewModels
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host != null)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
                _host.Dispose();
            }
            base.OnExit(e);
        }
    }

}
