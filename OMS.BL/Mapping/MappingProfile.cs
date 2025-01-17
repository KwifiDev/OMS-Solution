using AutoMapper;
using OMS.BL.Dtos.Tables;
using OMS.BL.Dtos.Views;
using OMS.DA.Entities;
using OMS.DA.Views;

namespace OMS.BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();

            CreateMap<Branch, BranchDto>().ReverseMap();

            CreateMap<Client, ClientDto>().ReverseMap();

            CreateMap<Debt, DebtDto>().ReverseMap();

            CreateMap<Discount, DiscountDto>().ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();

            CreateMap<PermissionsConfig, PermissionsConfigDto>().ReverseMap();

            CreateMap<Person, PersonDto>().ReverseMap();

            CreateMap<Revenue, RevenueDto>().ReverseMap();

            CreateMap<Sale, SaleDto>().ReverseMap();

            CreateMap<Service, ServiceDto>().ReverseMap();

            CreateMap<Transaction, TransactionDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<AccountBalancesTransaction, AccountBalancesTransactionDto>().ReverseMap();

            CreateMap<BranchOperationalMetric, BranchOperationalMetricDto>().ReverseMap();

            CreateMap<ClientDetail, ClientDetailDto>().ReverseMap();

            CreateMap<ClientsByType, ClientsByTypeDto>().ReverseMap();

            CreateMap<DebtsByStatus, DebtsByStatusDto>().ReverseMap();

            CreateMap<DebtsSummary, DebtsSummaryDto>().ReverseMap();

            CreateMap<DiscountsApplied, DiscountsAppliedDto>().ReverseMap();

            CreateMap<MonthlyFinancialSummary, MonthlyFinancialSummaryDto>().ReverseMap();

            CreateMap<PaymentsSummary, PaymentsSummaryDto>().ReverseMap();

            CreateMap<SalesSummary, SalesSummaryDto>().ReverseMap();

            CreateMap<TransactionsByType, TransactionsByTypeDto>().ReverseMap();

            CreateMap<TransactionsSummary, TransactionsSummaryDto>().ReverseMap();

            CreateMap<UserAccount, UserAccountDto>().ReverseMap();

            CreateMap<UserDetail, UserDetailDto>().ReverseMap();

        }
    }
}
