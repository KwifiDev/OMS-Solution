using AutoMapper;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.StoredProcedureParams;
using OMS.API.Dtos.Tables;
using OMS.API.Dtos.Views;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.BL.Models.Views;

namespace OMS.API.Mapping
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<AccountModel, AccountDto>().ReverseMap();

            CreateMap<AccountTransactionDto, AccountTransactionModel>().ReverseMap();

            CreateMap<BranchModel, BranchDto>().ReverseMap();

            CreateMap<BranchOptionModel, BranchOptionDto>().ReverseMap();

            CreateMap<ClientModel, ClientDto>().ReverseMap();

            CreateMap<DebtModel, DebtDto>().ReverseMap();

            CreateMap<DiscountModel, DiscountDto>().ReverseMap();

            CreateMap<PaymentModel, PaymentDto>().ReverseMap();

            CreateMap<PersonModel, PersonDto>().ReverseMap();

            CreateMap<RevenueModel, RevenueDto>().ReverseMap();

            CreateMap<SaleModel, SaleDto>().ReverseMap();

            CreateMap<SaleCreationModel, SaleCreationDto>().ReverseMap();

            CreateMap<ServiceModel, ServiceDto>().ReverseMap();

            CreateMap<ServiceOptionModel, ServiceOptionDto>().ReverseMap();

            CreateMap<ServicesSummaryModel, ServicesSummaryDto>().ReverseMap();

            CreateMap<TransactionModel, TransactionDto>().ReverseMap();

            CreateMap<UserModel, UserDto>().ReverseMap();

            CreateMap<AccountBalancesTransactionModel, AccountBalancesTransactionDto>().ReverseMap();

            CreateMap<BranchOperationalMetricModel, BranchOperationalMetricDto>().ReverseMap();

            CreateMap<PersonDetailModel, PersonDetailDto>().ReverseMap();

            CreateMap<ClientDetailModel, ClientDetailDto>().ReverseMap();

            CreateMap<ClientsSummaryModel, ClientsSummaryDto>().ReverseMap();

            CreateMap<ClientsByTypeModel, ClientsByTypeDto>().ReverseMap();

            CreateMap<DebtsByStatusModel, DebtsByStatusDto>().ReverseMap();

            CreateMap<DebtsSummaryModel, DebtsSummaryDto>().ReverseMap();

            CreateMap<DebtCreationModel, DebtCreationDto>().ReverseMap();

            CreateMap<DiscountsAppliedModel, DiscountsAppliedDto>()
                .ForSourceMember(src => src.ServiceId, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<MonthlyFinancialSummaryModel, MonthlyFinancialSummaryDto>().ReverseMap();

            CreateMap<PaymentsSummaryModel, PaymentsSummaryDto>().ReverseMap();

            CreateMap<SalesSummaryModel, SalesSummaryDto>()
                .ForSourceMember(src => src.ClientId, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<TransactionsByTypeModel, TransactionsByTypeDto>().ReverseMap();

            CreateMap<TransactionsSummaryModel, TransactionsSummaryDto>()
                .ForSourceMember(src => src.AccountId, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<UserAccountModel, UserAccountDto>().ReverseMap();

            CreateMap<UserDetailModel, UserDetailDto>().ReverseMap();

            CreateMap<UserLoginModel, ResponseLoginDto>().ReverseMap();

            CreateMap<PayDebtModel, PayDebtDto>().ReverseMap();

            CreateMap<PayDebtsModel, PayDebtsDto>().ReverseMap();

            CreateMap<PaymentsSummaryModel, PaymentsSummaryDto>()
                .ForSourceMember(src => src.AccountId, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<DashboardSummaryModel, DashboardSummaryDto>().ReverseMap();

            CreateMap<ChangePasswordModel, ChangePasswordDto>().ReverseMap();

            CreateMap<LoginModel, LoginDto>().ReverseMap();

            CreateMap<CheckDiscountAppliedModel, DiscountDto>()
                .ForMember(dest => dest.DiscountId, opt => opt.Ignore())
                .ForMember(dest => dest.DiscountPercentage, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterModel, UserDto>().ReverseMap();

            CreateMap<RegisterDto, FullRegisterModel>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore());


            CreateMap<RegisterDto, PersonModel>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? null));

            CreateMap<RoleDto, RoleModel>().ReverseMap();

            CreateMap<RolesSummaryDto, RolesSummaryModel>().ReverseMap();

            CreateMap<UserRoleDto, UserRoleModel>().ReverseMap();

            CreateMap<InputUserRolesDto, InputUserRolesModel>().ReverseMap();

            CreateMap<RoleClaimDto, RoleClaimModel>().ReverseMap();

            CreateMap<PermissionDto, PermissionModel>().ReverseMap();
        }
    }
}
