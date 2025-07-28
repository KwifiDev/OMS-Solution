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

            CreateMap<Person, PersonModel>().ReverseMap();

            CreateMap<Revenue, RevenueModel>().ReverseMap();

            CreateMap<Sale, SaleModel>().ReverseMap();

            CreateMap<Service, ServiceModel>().ReverseMap();

            CreateMap<Service, ServiceOptionModel>().ReverseMap();

            CreateMap<ServicesSummary, ServicesSummaryModel>().ReverseMap();

            CreateMap<Transaction, TransactionModel>().ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

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
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(scr => scr.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(scr => scr.Person.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(scr => scr.Person.Gender))
                .ReverseMap();

            CreateMap<DashboardSummary, DashboardSummaryModel>().ReverseMap();

            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<FullRegisterModel, PersonModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));


            CreateMap<FullRegisterModel, RegisterModel>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<Role, RoleModel>().ReverseMap();

            CreateMap<RolesSummary, RolesSummaryModel>().ReverseMap();
        }
    }
}
