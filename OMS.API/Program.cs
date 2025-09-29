using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OMS.API.Extensions;
using OMS.API.Mapping;
using OMS.API.Models.Settings;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Settings;
using OMS.BL.Services.Security;
using OMS.BL.Services.Tables;
using OMS.BL.Services.Views;
using OMS.Common.Data;
using OMS.DA.Context;
using OMS.DA.Entities.Identity;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Repositories.EntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Seeders;
using OMS.DA.UOW;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5000); // HTTP
        options.ListenAnyIP(5001, listen =>
            listen.UseHttps(Path.Combine(AppContext.BaseDirectory, "LocalApi.pfx"), "123456")); // HTTPS
    });
}


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new AuthorizeCrudConvetion());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "JWT Bearer Token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OMS.API",
        Version = "v1",
        Description = "Office Management System API Documentation",
        Contact = new OpenApiContact
        {
            Name = "KwifiDev",
            Email = "Alkwifi.win@outlook.com"
        }
    });

    // Include documentation comments from XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    // Enable displaying enums as text values
    options.SchemaFilter<EnumSchemaFilter>();

    // Sort endpoints alphabetically
    options.OrderActionsBy(api =>
    {
        // If the controller is named Auth, we put it first
        if (api.ActionDescriptor.RouteValues["controller"] == "Auth")
        {
            return "000_" + api.HttpMethod + api.RelativePath;
        }

        // The remainder in natural order
        return api.HttpMethod + api.RelativePath;
    });

    options.OperationFilter<TenantHeaderOperationFilter>();
});

builder.Services.Configure<SwaggerGeneratorOptions>(options =>
{
    options.InferSecuritySchemes = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<TenantSettings>(builder.Configuration.GetSection(nameof(TenantSettings)));
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
    options.UseSqlServer(tenantProvider.CurrentTenant.ConnectionString);
});

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

builder.Services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
builder.Services.AddScoped<IRoleClaimService, RoleClaimService>();

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddScoped<ITokenService, TokenService>();



builder.Services.AddMemoryCache(options => options.TrackStatistics = true);
builder.Services.AddSingleton<IPermissionCacheService, PermissionCacheService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddScoped<ClearPermissionCacheFilter>();

builder.Services.Configure<SwaggerSettings>(builder.Configuration.GetSection(nameof(SwaggerSettings)));

// Mapping jwt configs to JwtSettings Object
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };

    // Error Handling
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Append("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var permissions = PermissionsData.GetAll();
    var authorizationOptions = serviceProvider.GetRequiredService<IOptions<AuthorizationOptions>>();

    foreach (var permission in permissions)
    {
        authorizationOptions.Value.AddPolicy(permission, policy =>
            policy.Requirements.Add(new PermissionRequirement(permission)));
    }


    var tenants = serviceProvider.GetRequiredService<IOptions<TenantSettings>>().Value.Tenants;

    var tasks = tenants.Select(async tenant =>
    {
        await using var seedScope = serviceProvider.CreateAsyncScope();
        try
        {
            var tenantProvider = seedScope.ServiceProvider.GetRequiredService<ITenantProvider>();
            tenantProvider.SetTenant(tenant);

            await DataSeeder.SeedDataAsync(
                seedScope.ServiceProvider.GetRequiredService<AppDbContext>(),
                seedScope.ServiceProvider.GetRequiredService<RoleManager<Role>>(),
                seedScope.ServiceProvider.GetRequiredService<UserManager<User>>()
            );
        }
        catch
        {
            // Logger Here
        }
    });

    await Task.WhenAll(tasks);
}

var swaggerSettings = app.Services.GetRequiredService<IOptions<SwaggerSettings>>().Value;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || swaggerSettings.EnableInProduction)
{
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            swagger.Servers = new List<OpenApiServer>
            {
                new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" }
            };
        });
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OMS API v1");
        options.RoutePrefix = swaggerSettings.CustomPath ?? "swagger";
        options.DocumentTitle = "OMS API Documentation";
        options.DefaultModelsExpandDepth(2);
        options.DefaultModelExpandDepth(2);
        options.DisplayOperationId();
        options.DisplayRequestDuration();
        options.EnableDeepLinking();
        options.EnableFilter();
        options.ShowExtensions();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<TenantMiddleware>(); // This Middleware must be here
app.UseAuthorization();

app.MapControllers();

app.Run();
