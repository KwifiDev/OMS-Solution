using AutoMapper;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OMS.UI.APIs.Services.Connection;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.APIs.Services.Security;
using OMS.UI.APIs.Services.Tables;
using OMS.UI.APIs.Services.Views;
using OMS.UI.Mapping;
using OMS.UI.Services.Authentication;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Hash;
using OMS.UI.Services.JWT;
using OMS.UI.Services.Loading;
using OMS.UI.Services.Navigation;
using OMS.UI.Services.Registry;
using OMS.UI.Services.Settings;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.ViewModels.UserControls;
using OMS.UI.ViewModels.UserControls.Interfaces;
using OMS.UI.ViewModels.Windows;
using OMS.UI.ViewModels.Windows.AddEditViewModel;
using OMS.UI.Views;
using OMS.UI.Views.Pages;
using OMS.UI.Views.Windows;
using OMS.UI.Views.Windows.AddEditWindow;
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
                    RegisterApiServices(services);
                    RegisterDatabaseConnection(services, context.Configuration);
                    RegisterServices(services);
                    RegisterMapper(services);
                    RegisterViewModels(services);
                    RegisterViews(services);
                    RegisterMVVMServices(services);

                    // Initialize the IoC container
                    Ioc.Default.ConfigureServices(services.BuildServiceProvider());
                });

        private static void RegisterApiServices(IServiceCollection services)
        {
            services.AddTransient<AuthHeaderHandler>();

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7012/");
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();

            services.AddTransient(typeof(IGenericViewApiService<,>), typeof(GenericViewApiService<,>));
            services.AddTransient(typeof(IGenericApiService<,>), typeof(GenericApiService<,>));
        }

        private static void RegisterDatabaseConnection(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DbConnection");
            // Here we will Send Database Con to ServerSide and Using (Multi-Tenant Database Design) and (Dynamic Connection Strings)
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();

            services.AddTransient<IBranchService, BranchService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserDetailService, UserDetailService>();

            services.AddTransient<IPersonDetailService, PersonDetailService>();

            services.AddTransient<IBranchOperationalMetricService, BranchOperationalMetricService>();

            services.AddTransient<IClientService, ClientService>();

            services.AddTransient<IClientsSummaryService, ClientsSummaryService>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUserAccountService, UserAccountService>();

            services.AddTransient<ITransactionsSummaryService, TransactionsSummaryService>();

            services.AddTransient<IServiceService, ServiceService>();

            services.AddTransient<IServicesSummaryService, ServicesSummaryService>();

            services.AddTransient<IDiscountService, DiscountService>();

            services.AddTransient<IDiscountsAppliedService, DiscountsAppliedService>();

            services.AddTransient<ISaleService, SaleService>();

            services.AddTransient<ISalesSummaryService, SalesSummaryService>();

            services.AddTransient<IDebtService, DebtService>();

            services.AddTransient<IDebtsSummaryService, DebtsSummaryService>();

            services.AddTransient<IPaymentsSummaryService, PaymentsSummaryService>();

            services.AddTransient<IRevenueService, RevenueService>();

            services.AddTransient<IDashboardSummaryService, DashboardSummaryService>();

            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IRolesSummaryService, RolesSummaryService>();

            services.AddTransient<IRoleClaimService, RoleClaimService>();

            services.AddTransient<IPermissionService, PermissionService>();

            //services.AddTransient<ITokenService, TokenService>();
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UIMappingProfile());
            }, loggerFactory);
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddSingleton<LoginViewModel>();

            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<DashboardPageViewModel>();

            services.AddSingleton<PeoplePageViewModel>();
            services.AddTransient<PersonDetailsViewModel>();
            services.AddTransient<AddEditPersonViewModel>();

            services.AddSingleton<BranchesPageViewModel>();

            services.AddTransient<AddEditBranchViewModel>();

            services.AddSingleton<UsersPageViewModel>();

            services.AddTransient<IFindPersonViewModel, FindPersonViewModel>();

            services.AddTransient<AddEditUserViewModel>();

            services.AddSingleton<ClientsPageViewModel>();

            services.AddTransient<AddEditClientViewModel>();

            services.AddTransient<ClientAccountDetailsViewModel>();

            services.AddTransient<ClientAccountTransactionViewModel>();

            services.AddTransient<AccountTransactionsViewModel>();

            services.AddTransient<ServicesPageViewModel>();

            services.AddTransient<AddEditServiceViewModel>();

            services.AddTransient<AddEditDiscountViewModel>();

            services.AddTransient<DiscountsAppliedViewModel>();

            services.AddTransient<SalesSummaryViewModel>();

            services.AddTransient<AddEditSaleViewModel>();

            services.AddTransient<DebtsSummaryViewModel>();

            services.AddTransient<AddEditDebtViewModel>();

            services.AddTransient<PayDebtViewModel>();

            services.AddTransient<AccountPaymentsViewModel>();

            services.AddTransient<RevenuesPageViewModel>();

            services.AddTransient<AddEditRevenueViewModel>();

            services.AddSingleton<SettingsPageViewModel>();

            services.AddTransient<ChangePasswordViewModel>();

            services.AddTransient<StartupViewModel>();

            services.AddTransient<RolesSummaryViewModel>();

            services.AddTransient<AddEditRoleViewModel>();

            services.AddTransient<UserRolesViewModel>();

            services.AddTransient<RoleClaimsViewModel>();

            services.AddTransient<AddEditRoleClaimViewModel>();
        }

        private static void RegisterViews(IServiceCollection services)
        {
            services.AddSingleton(provider =>
                new LoginWindow { DataContext = provider.GetRequiredService<LoginViewModel>() });

            services.AddTransient(provider =>
                new MainWindow { DataContext = provider.GetRequiredService<MainWindowViewModel>() });

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

            services.AddTransient(provider =>
                new AddEditBranchWindow { DataContext = provider.GetRequiredService<AddEditBranchViewModel>() });

            services.AddSingleton(provider =>
                new UsersPage { DataContext = provider.GetRequiredService<UsersPageViewModel>() });

            services.AddTransient(provider =>
                new AddEditUserWindow { DataContext = provider.GetRequiredService<AddEditUserViewModel>() });

            services.AddSingleton(provider =>
                new ClientsPage { DataContext = provider.GetRequiredService<ClientsPageViewModel>() });

            services.AddTransient(provider =>
                new AddEditClientWindow { DataContext = provider.GetRequiredService<AddEditClientViewModel>() });

            services.AddTransient(provider =>
                new ClientAccountDetailsWindow { DataContext = provider.GetRequiredService<ClientAccountDetailsViewModel>() });

            services.AddTransient(provider =>
                new ClientAccountTransactionWindow { DataContext = provider.GetRequiredService<ClientAccountTransactionViewModel>() });

            services.AddTransient(provider =>
                new AccountTransactionsWindow { DataContext = provider.GetRequiredService<AccountTransactionsViewModel>() });

            services.AddTransient(provider =>
                new ServicesPage { DataContext = provider.GetRequiredService<ServicesPageViewModel>() });

            services.AddTransient(provider =>
                new AddEditServiceWindow { DataContext = provider.GetRequiredService<AddEditServiceViewModel>() });

            services.AddTransient(provider =>
                new AddEditDiscountWindow { DataContext = provider.GetRequiredService<AddEditDiscountViewModel>() });

            services.AddTransient(provider =>
                new DiscountsAppliedWindow { DataContext = provider.GetRequiredService<DiscountsAppliedViewModel>() });

            services.AddTransient(provider =>
                new SalesSummaryWindow { DataContext = provider.GetRequiredService<SalesSummaryViewModel>() });

            services.AddTransient(provider =>
                new AddEditSaleWindow { DataContext = provider.GetRequiredService<AddEditSaleViewModel>() });

            services.AddTransient(provider =>
                new DebtsSummaryWindow { DataContext = provider.GetRequiredService<DebtsSummaryViewModel>() });

            services.AddTransient(provider =>
                new AddEditDebtWindow { DataContext = provider.GetRequiredService<AddEditDebtViewModel>() });

            services.AddTransient(provider =>
                new PayDebtWindow { DataContext = provider.GetRequiredService<PayDebtViewModel>() });

            services.AddTransient(provider =>
                new AccountPaymentsWindow { DataContext = provider.GetRequiredService<AccountPaymentsViewModel>() });

            services.AddTransient(provider =>
                new RevenuesPage { DataContext = provider.GetRequiredService<RevenuesPageViewModel>() });

            services.AddTransient(provider =>
                new AddEditRevenueWindow { DataContext = provider.GetRequiredService<AddEditRevenueViewModel>() });

            services.AddSingleton(provider =>
                new SettingsPage { DataContext = provider.GetRequiredService<SettingsPageViewModel>() });

            services.AddTransient(provider =>
                new ChangePasswordWindow { DataContext = provider.GetRequiredService<ChangePasswordViewModel>() });

            services.AddTransient(provider =>
                new StartupWindow { DataContext = provider.GetRequiredService<StartupViewModel>() });

            services.AddTransient(provider =>
                new RolesSummaryWindow { DataContext = provider.GetRequiredService<RolesSummaryViewModel>() });

            services.AddTransient(provider =>
                new AddEditRoleWindow { DataContext = provider.GetRequiredService<AddEditRoleViewModel>() });

            services.AddTransient(provider =>
                new UserRolesWindow { DataContext = provider.GetRequiredService<UserRolesViewModel>() });

            services.AddTransient(provider =>
                new RoleClaimsWindow { DataContext = provider.GetRequiredService<RoleClaimsViewModel>() });

            services.AddTransient(provider =>
                new AddEditRoleClaimWindow { DataContext = provider.GetRequiredService<AddEditRoleClaimViewModel>() });

        }

        private static void RegisterMVVMServices(IServiceCollection services)
        {
            services.AddTransient<IDialogService, DialogService>();

            services.AddSingleton<IMessageService, MessageService>();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IWindowService, WindowService>();

            services.AddTransient<IStatusService, StatusService>();

            services.AddSingleton<IUserSessionService, UserSessionService>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<ISettingsService, SettingsService>();

            services.AddTransient<IRegistryService, RegistryService>();

            services.AddSingleton<IHashService, HashService>();

            services.AddTransient<IConnectionService, ConnectionService>();

            services.AddTransient<ILoadingService, LoadingService>();

            services.AddTransient<IJwtPayloadService, JwtPayloadService>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var startupWindow = Ioc.Default.GetRequiredService<StartupWindow>();
            startupWindow.Show();

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
