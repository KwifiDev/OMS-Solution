using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OMS.API.Mapping;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Services.Tables;
using OMS.BL.Services.Views;
using OMS.DA.Context;
using OMS.DA.Entities.Identity;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Repositories.EntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.UOW;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);

builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, Role>>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var configExpression = new MapperConfigurationExpression();
configExpression.AddProfile(new APIMappingProfile());
configExpression.AddProfile(new BLMappingProfile());
ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<IMapperService, MapperService>();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericViewRepository<>), typeof(GenericViewRepository<>));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchService, BranchService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserDetailRepository, UserDetailRepository>();
builder.Services.AddScoped<IUserDetailService, UserDetailService>();

builder.Services.AddScoped<IPersonDetailRepository, PersonDetailRepository>();
builder.Services.AddScoped<IPersonDetailService, PersonDetailService>();

builder.Services.AddScoped<IBranchOperationalMetricRepository, BranchOperationalMetricRepository>();
builder.Services.AddScoped<IBranchOperationalMetricService, BranchOperationalMetricService>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IClientsSummaryRepository, ClientsSummaryRepository>();
builder.Services.AddScoped<IClientsSummaryService, ClientsSummaryService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<ITransactionsSummaryRepository, TransactionsSummaryRepository>();
builder.Services.AddScoped<ITransactionsSummaryService, TransactionsSummaryService>();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddScoped<IServicesSummaryRepository, ServicesSummaryRepository>();
builder.Services.AddScoped<IServicesSummaryService, ServicesSummaryService>();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

builder.Services.AddScoped<IDiscountsAppliedRepository, DiscountsAppliedRepository>();
builder.Services.AddScoped<IDiscountsAppliedService, DiscountsAppliedService>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddScoped<ISalesSummaryRepository, SalesSummaryRepository>();
builder.Services.AddScoped<ISalesSummaryService, SalesSummaryService>();

builder.Services.AddScoped<IDebtRepository, DebtRepository>();
builder.Services.AddScoped<IDebtService, DebtService>();

builder.Services.AddScoped<IDebtsSummaryRepository, DebtsSummaryRepository>();
builder.Services.AddScoped<IDebtsSummaryService, DebtsSummaryService>();

builder.Services.AddScoped<IPaymentsSummaryRepository, PaymentsSummaryRepository>();
builder.Services.AddScoped<IPaymentsSummaryService, PaymentsSummaryService>();

builder.Services.AddScoped<IRevenueRepository, RevenueRepository>();
builder.Services.AddScoped<IRevenueService, RevenueService>();

builder.Services.AddScoped<IDashboardSummaryRepository, DashboardSummaryRepository>();
builder.Services.AddScoped<IDashboardSummaryService, DashboardSummaryService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IRolesSummaryRepository, RolesSummaryRepository>();
builder.Services.AddScoped<IRolesSummaryService, RolesSummaryService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
