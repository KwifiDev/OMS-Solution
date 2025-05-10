using AutoMapper;
using OMS.API.Mapping;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Services.Tables;
using OMS.BL.Services.Views;
using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Repositories.EntityRepos;
using OMS.DA.Repositories.ViewRepos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new APIMappingProfile());
    mc.AddProfile(new BLMappingProfile());
});
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
