using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Configurations.EntitiesConfigurations;
using OMS.DA.Configurations.IdentitiesConfigurations;
using OMS.DA.Configurations.ViewsConfigurations;
using OMS.DA.Entities;
using OMS.DA.Views;

namespace OMS.DA.Context;

public partial class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountBalancesTransaction> AccountBalancesTransactions { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchOperationalMetric> BranchOperationalMetrics { get; set; }

    public virtual DbSet<PersonDetail> PersonDetails { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientDetail> ClientDetails { get; set; }

    public virtual DbSet<ClientsSummary> ClientsSummaries { get; set; }

    public virtual DbSet<ClientsByType> ClientsByTypes { get; set; }

    public virtual DbSet<Debt> Debts { get; set; }

    public virtual DbSet<DebtsByStatus> DebtsByStatuses { get; set; }

    public virtual DbSet<DebtsSummary> DebtsSummaries { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountsApplied> DiscountsApplieds { get; set; }

    public virtual DbSet<MonthlyFinancialSummary> MonthlyFinancialSummaries { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentsSummary> PaymentsSummaries { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Revenue> Revenues { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesSummary> SalesSummaries { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServicesSummary> ServicesSummaries { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionsByType> TransactionsByTypes { get; set; }

    public virtual DbSet<TransactionsSummary> TransactionsSummaries { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<DashboardSummary> DashboardSummaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OMS;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        IdentityConfig.ConfigureAll(modelBuilder);

        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.ApplyConfiguration(new AccountConfig());

        modelBuilder.ApplyConfiguration(new AccountBalancesTransactionConfig());

        modelBuilder.ApplyConfiguration(new BranchConfig());

        modelBuilder.ApplyConfiguration(new BranchOperationalMetricConfig());

        modelBuilder.ApplyConfiguration(new PersonDetailConfig());

        modelBuilder.ApplyConfiguration(new ClientConfig());

        modelBuilder.ApplyConfiguration(new ClientDetailConfig());

        modelBuilder.ApplyConfiguration(new ClientsSummaryConfig());

        modelBuilder.ApplyConfiguration(new ClientsByTypeConfig());

        modelBuilder.ApplyConfiguration(new DebtConfig());

        modelBuilder.ApplyConfiguration(new DebtsByStatusConfig());

        modelBuilder.ApplyConfiguration(new DebtsSummaryConfig());

        modelBuilder.ApplyConfiguration(new DiscountConfig());

        modelBuilder.ApplyConfiguration(new DiscountsAppliedConfig());

        modelBuilder.ApplyConfiguration(new MonthlyFinancialSummaryConfig());

        modelBuilder.ApplyConfiguration(new PaymentConfig());

        modelBuilder.ApplyConfiguration(new PaymentsSummaryConfig());

        modelBuilder.ApplyConfiguration(new PersonConfig());

        modelBuilder.ApplyConfiguration(new RevenueConfig());

        modelBuilder.ApplyConfiguration(new SaleConfig());

        modelBuilder.ApplyConfiguration(new SalesSummaryConfig());

        modelBuilder.ApplyConfiguration(new ServiceConfig());

        modelBuilder.ApplyConfiguration(new ServicesSummaryConfig());

        modelBuilder.ApplyConfiguration(new TransactionConfig());

        modelBuilder.ApplyConfiguration(new TransactionsByTypeConfig());

        modelBuilder.ApplyConfiguration(new TransactionsSummaryConfig());

        modelBuilder.ApplyConfiguration(new UserConfig());

        modelBuilder.ApplyConfiguration(new UserAccountConfig());

        modelBuilder.ApplyConfiguration(new UserDetailConfig());

        modelBuilder.ApplyConfiguration(new DashboardSummaryConfig());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
