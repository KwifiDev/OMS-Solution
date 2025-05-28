using AutoMapper;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.BL.Models.Views;
using OMS.DA.Entities;
using OMS.DA.Views;

namespace OMS.BL.Mapping
{
    public class BLMappingProfile : Profile
    {
        public BLMappingProfile()
        {
            CreateMap<Account, AccountModel>().ReverseMap();

            CreateMap<Branch, BranchModel>().ReverseMap();

            CreateMap<Branch, BranchOptionModel>().ReverseMap();

            CreateMap<Client, ClientModel>().ReverseMap();

            CreateMap<Debt, DebtModel>().ReverseMap();

            CreateMap<Discount, DiscountModel>().ReverseMap();

            CreateMap<Payment, PaymentModel>().ReverseMap();

            CreateMap<PermissionsConfig, PermissionsConfigModel>().ReverseMap();

            CreateMap<Person, PersonModel>().ReverseMap();

            CreateMap<Revenue, RevenueModel>().ReverseMap();

            CreateMap<Sale, SaleModel>().ReverseMap();

            CreateMap<Service, ServiceModel>().ReverseMap();

            CreateMap<ServicesSummary, ServicesSummaryModel>().ReverseMap();

            CreateMap<Transaction, TransactionModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<AccountBalancesTransaction, AccountBalancesTransactionModel>().ReverseMap();

            CreateMap<BranchOperationalMetric, BranchOperationalMetricModel>().ReverseMap();

            CreateMap<PersonDetail, PersonDetailModel>().ReverseMap();

            CreateMap<ClientDetail, ClientDetailModel>().ReverseMap();

            CreateMap<ClientsSummary, ClientsSummaryModel>().ReverseMap();

            CreateMap<ClientsByType, ClientsByTypeModel>().ReverseMap();

            CreateMap<DebtsByStatus, DebtsByStatusModel>().ReverseMap();

            CreateMap<DebtsSummary, DebtsSummaryModel>().ReverseMap();

            CreateMap<DiscountsApplied, DiscountsAppliedModel>().ReverseMap();

            CreateMap<MonthlyFinancialSummary, MonthlyFinancialSummaryModel>().ReverseMap();

            CreateMap<PaymentsSummary, PaymentsSummaryModel>().ReverseMap();

            CreateMap<SalesSummary, SalesSummaryModel>().ReverseMap();

            CreateMap<TransactionsByType, TransactionsByTypeModel>().ReverseMap();

            CreateMap<TransactionsSummary, TransactionsSummaryModel>().ReverseMap();

            CreateMap<UserAccount, UserAccountModel>().ReverseMap();

            CreateMap<UserDetail, UserDetailModel>().ReverseMap();

            CreateMap<User, UserLoginModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(scr => scr.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(scr => scr.Person.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(scr => scr.Person.Gender))
                .ReverseMap();

        }
    }
}
