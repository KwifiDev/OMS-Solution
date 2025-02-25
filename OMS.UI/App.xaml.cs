﻿using AutoMapper;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Services.Tables;
using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.EntityRepos;
using OMS.UI.Mapping;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Navigation;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.ViewModels.Windows;
using OMS.UI.Views;
using OMS.UI.Views.Pages;
using OMS.UI.Views.Windows;
using System.Windows;

namespace OMS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host = null!;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
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
                    RegisterViews(services);
                    RegisterMVVMServices(services);

                    // Initialize the IoC container
                    Ioc.Default.ConfigureServices(services.BuildServiceProvider());
                });

        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Register other repositories
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPersonService, PersonService>();
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BLMappingProfile());
                mc.AddProfile(new UIMappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IMapperService, MapperService>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<DashboardPageViewModel>();

            services.AddSingleton<PeoplePageViewModel>();
            services.AddTransient<PersonDetailsViewModel>();
            services.AddTransient<AddEditPersonViewModel>();

            services.AddSingleton<BranchesPageViewModel>();

            services.AddSingleton<UsersPageViewModel>();

        }

        private static void RegisterViews(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton(provider => 
                new DashboardPage { DataContext = provider.GetRequiredService<DashboardPageViewModel>() });

            services.AddSingleton(provider =>
                new PeoplePage { DataContext = provider.GetRequiredService<PeoplePageViewModel>() });

            services.AddTransient(provider =>
                new PersonDetailsWindow { DataContext = provider.GetRequiredService<PersonDetailsViewModel>() });

            services.AddTransient(provider =>
                new AddEditPersonWindow { DataContext = provider.GetRequiredService<AddEditPersonViewModel>() });

            services.AddSingleton(provider =>
                new BranchesPage { DataContext = provider.GetRequiredService<BranchesPageViewModel>() });

            services.AddSingleton(provider =>
                new UsersPage { DataContext = provider.GetRequiredService<UsersPageViewModel>() });
        }

        private static void RegisterMVVMServices(IServiceCollection services)
        {
            services.AddTransient<IDialogService, DialogService>();

            services.AddSingleton<IMessageService, MessageService>();

            services.AddSingleton<INavigationService, NavigationService>(provider =>
                new NavigationService(provider.GetRequiredService<MainWindow>().mainFrame));

            services.AddSingleton<IWindowService, WindowService>();
        }

        private async Task TryConnectToDBAsync()
        {
            try
            {
                using var scope = Ioc.Default.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!await dbContext.Database.CanConnectAsync())
                {
                    MessageBox.Show("Failed to connect to the database.", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to connect to the database: {ex.Message}", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            await TryConnectToDBAsync();
            MainWindow = Ioc.Default.GetRequiredService<MainWindow>();
            MainWindow.DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
            _host.Dispose();

            base.OnExit(e);
        }
    }

}
