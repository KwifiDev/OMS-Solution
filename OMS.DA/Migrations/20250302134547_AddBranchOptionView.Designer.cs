﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMS.DA.Context;

#nullable disable

namespace OMS1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250302134547_AddBranchOptionView")]
    partial class AddBranchOptionView
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Arabic_CI_AS")
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OMS.DA.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("UserAccount")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AccountId")
                        .HasName("accounts_accountid_primary");

                    b.HasIndex(new[] { "ClientId" }, "accounts_clientid_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "UserAccount" }, "accounts_useraccount_unique")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("OMS.DA.Entities.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BranchId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("BranchId")
                        .HasName("branches_branchid_primary");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("OMS.DA.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<byte>("ClientType")
                        .HasColumnType("tinyint")
                        .HasComment("0 = Normal | 1 = Lawyer | 2 = Other");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("ClientId")
                        .HasName("clients_clientid_primary");

                    b.HasIndex(new[] { "PersonId" }, "clients_personid_unique")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("OMS.DA.Entities.Debt", b =>
                {
                    b.Property<int>("DebtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DebtId"));

                    b.Property<decimal?>("AmountDeducted")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<DateOnly>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Notes")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasComment("0 = NotPaid | 1 = Paid | 2 = Canceled");

                    b.Property<decimal?>("Total")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(14, 2)")
                        .HasComputedColumnSql("([Cost]*[Quantity])", true);

                    b.HasKey("DebtId")
                        .HasName("debts_debtid_primary");

                    b.HasIndex("ClientId");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Debts", t =>
                        {
                            t.HasTrigger("TR_InsertNewDebt");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("OMS.DA.Entities.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<byte>("ClientType")
                        .HasColumnType("tinyint")
                        .HasComment("0 = Normal | 1 = Lawyer | 2 = Other");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("DiscountId")
                        .HasName("discounts_discountid_primary");

                    b.HasIndex(new[] { "ServiceId", "ClientType" }, "unique_service_client")
                        .IsUnique();

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("OMS.DA.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<DateOnly>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PaymentId")
                        .HasName("payments_paymentid_primary");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("OMS.DA.Entities.PermissionsConfig", b =>
                {
                    b.Property<int>("PermissionConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionConfigId"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PermissionNo")
                        .HasColumnType("int")
                        .HasComment("Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...");

                    b.HasKey("PermissionConfigId")
                        .HasName("permissionsconfig_permissionconfigid_primary");

                    b.ToTable("PermissionsConfig");
                });

            modelBuilder.Entity("OMS.DA.Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit")
                        .HasComment("0 = Male | 1 = Female");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("PersonId")
                        .HasName("people_personid_primary");

                    b.ToTable("People");
                });

            modelBuilder.Entity("OMS.DA.Entities.Revenue", b =>
                {
                    b.Property<int>("RevenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RevenueId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<DateOnly>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Notes")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RevenueId")
                        .HasName("revenues_revenueid_primary");

                    b.HasIndex(new[] { "CreatedAt" }, "revenues_createdat_unique")
                        .IsUnique();

                    b.ToTable("Revenues");
                });

            modelBuilder.Entity("OMS.DA.Entities.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"));

                    b.Property<decimal?>("AmountDeducted")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<DateOnly>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Notes")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasComment("0 = Uncompleted | 1 = Completed | 2 = Canceled");

                    b.Property<decimal?>("Total")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(14, 2)")
                        .HasComputedColumnSql("([Cost]*[Quantity])", true);

                    b.HasKey("SaleId")
                        .HasName("sales_saleid_primary");

                    b.HasIndex("ClientId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Sales", t =>
                        {
                            t.HasTrigger("TR_InsertNewSale");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("OMS.DA.Entities.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8, 2)");

                    b.HasKey("ServiceId")
                        .HasName("services_serviceid_primary");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("OMS.DA.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<DateOnly>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("TransactionType")
                        .HasColumnType("tinyint")
                        .HasComment("0 = Deposit | 1 = Withdraw | 2 = Transfer");

                    b.HasKey("TransactionId")
                        .HasName("transactions_transactionid_primary");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("OMS.DA.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("char(64)")
                        .IsFixedLength();

                    b.Property<int>("Permissions")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("UserId")
                        .HasName("users_userid_primary");

                    b.HasIndex("BranchId");

                    b.HasIndex(new[] { "PersonId" }, "users_personid_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "users_username_unique")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OMS.DA.Views.AccountBalancesTransaction", b =>
                {
                    b.Property<string>("AccountBalance")
                        .HasMaxLength(19)
                        .IsUnicode(false)
                        .HasColumnType("varchar(19)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<decimal?>("TotalTransactionAmount")
                        .HasColumnType("decimal(38, 2)");

                    b.Property<int?>("TotalTransactions")
                        .HasColumnType("int");

                    b.Property<string>("UserAccount")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.ToTable((string)null);

                    b.ToView("AccountBalancesTransactions", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.BranchOperationalMetric", b =>
                {
                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("TotalEmployees")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("BranchOperationalMetrics", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.ClientDetail", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.ToTable((string)null);

                    b.ToView("ClientDetails", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.ClientsByType", b =>
                {
                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<int?>("TotalClients")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("ClientsByType", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.DebtsByStatus", b =>
                {
                    b.Property<string>("DebtsStatus")
                        .IsRequired()
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("TotalAmount")
                        .HasMaxLength(34)
                        .IsUnicode(false)
                        .HasColumnType("varchar(34)");

                    b.Property<int?>("TotalDebts")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("DebtsByStatus", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.DebtsSummary", b =>
                {
                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<int>("DebtId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("TotalDebts")
                        .HasMaxLength(19)
                        .IsUnicode(false)
                        .HasColumnType("varchar(19)");

                    b.ToTable((string)null);

                    b.ToView("DebtsSummary", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.DiscountsApplied", b =>
                {
                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Discount")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("decimal(8, 2)");

                    b.ToTable((string)null);

                    b.ToView("DiscountsApplied", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.MonthlyFinancialSummary", b =>
                {
                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalRevenue")
                        .HasColumnType("decimal(38, 2)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("MonthlyFinancialSummary", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.PaymentsSummary", b =>
                {
                    b.Property<string>("AmountPaid")
                        .HasMaxLength(19)
                        .IsUnicode(false)
                        .HasColumnType("varchar(19)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("PaymentsSummary", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.SalesSummary", b =>
                {
                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SaleId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("TotalSales")
                        .HasMaxLength(19)
                        .IsUnicode(false)
                        .HasColumnType("varchar(19)");

                    b.ToTable((string)null);

                    b.ToView("SalesSummary", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.TransactionsByType", b =>
                {
                    b.Property<string>("TotalAmount")
                        .HasMaxLength(34)
                        .IsUnicode(false)
                        .HasColumnType("varchar(34)");

                    b.Property<int?>("TotalTransactions")
                        .HasColumnType("int");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.ToTable((string)null);

                    b.ToView("TransactionsByType", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.TransactionsSummary", b =>
                {
                    b.Property<string>("Amount")
                        .HasMaxLength(19)
                        .IsUnicode(false)
                        .HasColumnType("varchar(19)");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("UserAccount")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.ToTable((string)null);

                    b.ToView("TransactionsSummary", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.UserAccount", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("ClientBalance")
                        .HasMaxLength(19)
                        .IsUnicode(false)
                        .HasColumnType("varchar(19)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("UserAccount1")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("UserAccount");

                    b.ToTable((string)null);

                    b.ToView("UserAccounts", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Views.UserDetail", b =>
                {
                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(41)
                        .HasColumnType("nvarchar(41)");

                    b.Property<string>("IsActive")
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("varchar(9)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("WorkingBranch")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.ToTable((string)null);

                    b.ToView("UserDetails", (string)null);
                });

            modelBuilder.Entity("OMS.DA.Entities.Account", b =>
                {
                    b.HasOne("OMS.DA.Entities.Client", "Client")
                        .WithOne("Account")
                        .HasForeignKey("OMS.DA.Entities.Account", "ClientId")
                        .IsRequired()
                        .HasConstraintName("accounts_clientid_foreign");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("OMS.DA.Entities.Client", b =>
                {
                    b.HasOne("OMS.DA.Entities.Person", "Person")
                        .WithOne("Client")
                        .HasForeignKey("OMS.DA.Entities.Client", "PersonId")
                        .IsRequired()
                        .HasConstraintName("clients_personid_foreign");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("OMS.DA.Entities.Debt", b =>
                {
                    b.HasOne("OMS.DA.Entities.Client", "Client")
                        .WithMany("Debts")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("debts_clientid_foreign");

                    b.HasOne("OMS.DA.Entities.User", "CreatedByUser")
                        .WithMany("Debts")
                        .HasForeignKey("CreatedByUserId")
                        .IsRequired()
                        .HasConstraintName("debts_createdbyuserid_foreign");

                    b.HasOne("OMS.DA.Entities.Payment", "Payment")
                        .WithMany("Debts")
                        .HasForeignKey("PaymentId")
                        .HasConstraintName("debts_paymentid_foreign");

                    b.HasOne("OMS.DA.Entities.Service", "Service")
                        .WithMany("Debts")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("debts_serviceid_foreign");

                    b.Navigation("Client");

                    b.Navigation("CreatedByUser");

                    b.Navigation("Payment");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("OMS.DA.Entities.Discount", b =>
                {
                    b.HasOne("OMS.DA.Entities.Service", "Service")
                        .WithMany("Discounts")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("discounts_serviceid_foreign");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("OMS.DA.Entities.Payment", b =>
                {
                    b.HasOne("OMS.DA.Entities.Account", "Account")
                        .WithMany("Payments")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("payments_accountid_foreign");

                    b.HasOne("OMS.DA.Entities.User", "CreatedByUser")
                        .WithMany("Payments")
                        .HasForeignKey("CreatedByUserId")
                        .IsRequired()
                        .HasConstraintName("payments_createdbyuserid_foreign");

                    b.Navigation("Account");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("OMS.DA.Entities.Sale", b =>
                {
                    b.HasOne("OMS.DA.Entities.Client", "Client")
                        .WithMany("Sales")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("sales_clientid_foreign");

                    b.HasOne("OMS.DA.Entities.Service", "Service")
                        .WithMany("Sales")
                        .HasForeignKey("ServiceId")
                        .IsRequired()
                        .HasConstraintName("sales_serviceid_foreign");

                    b.Navigation("Client");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("OMS.DA.Entities.Transaction", b =>
                {
                    b.HasOne("OMS.DA.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("transactions_accountid_foreign");

                    b.HasOne("OMS.DA.Entities.User", "CreatedByUser")
                        .WithMany("Transactions")
                        .HasForeignKey("CreatedByUserId")
                        .IsRequired()
                        .HasConstraintName("transactions_createdbyuserid_foreign");

                    b.Navigation("Account");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("OMS.DA.Entities.User", b =>
                {
                    b.HasOne("OMS.DA.Entities.Branch", "Branch")
                        .WithMany("Users")
                        .HasForeignKey("BranchId")
                        .IsRequired()
                        .HasConstraintName("users_branchid_foreign");

                    b.HasOne("OMS.DA.Entities.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("OMS.DA.Entities.User", "PersonId")
                        .IsRequired()
                        .HasConstraintName("users_personid_foreign");

                    b.Navigation("Branch");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("OMS.DA.Entities.Account", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("OMS.DA.Entities.Branch", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("OMS.DA.Entities.Client", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Debts");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("OMS.DA.Entities.Payment", b =>
                {
                    b.Navigation("Debts");
                });

            modelBuilder.Entity("OMS.DA.Entities.Person", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OMS.DA.Entities.Service", b =>
                {
                    b.Navigation("Debts");

                    b.Navigation("Discounts");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("OMS.DA.Entities.User", b =>
                {
                    b.Navigation("Debts");

                    b.Navigation("Payments");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
