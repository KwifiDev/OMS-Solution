using AutoMapper;
using OMS.UI.APIs.Dtos.Hybrid;
using OMS.UI.APIs.Dtos.Hybrid.OMS.API.Dtos.Hybrid;
using OMS.UI.APIs.Dtos.StoredProcedureParams;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;


namespace OMS.UI.Mapping
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<PersonDto, PersonModel>().ReverseMap();
            CreateMap<BranchDto, BranchModel>().ReverseMap();
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<UserDetailDto, UserDetailModel>().ReverseMap();
            CreateMap<BranchOptionDto, BranchOptionModel>().ReverseMap();
            CreateMap<PersonDetailDto, PersonDetailModel>().ReverseMap();
            CreateMap<BranchOperationalMetricDto, BranchOperationalMetricModel>().ReverseMap();
            CreateMap<ResponseLoginDto, UserLoginModel>().ReverseMap();
            CreateMap<ClientDto, ClientModel>().ReverseMap();
            CreateMap<ClientsSummaryDto, ClientsSummaryModel>().ReverseMap();
            CreateMap<AccountDto, AccountModel>().ReverseMap();
            CreateMap<UserAccountDto, UserAccountModel>().ReverseMap();
            CreateMap<AccountTransactionDto, AccountTransactionModel>().ReverseMap();
            CreateMap<ResponseLoginDto, UserLoginModel>()
                .ForMember(dest => dest.FullName, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TransactionsSummaryDto, TransactionsSummaryModel>().ReverseMap();
            CreateMap<ServiceDto, ServiceModel>().ReverseMap();
            CreateMap<ServicesSummaryDto, ServicesSummaryModel>().ReverseMap();
            CreateMap<DiscountDto, DiscountModel>().ReverseMap();
            CreateMap<DiscountsAppliedDto, DiscountsAppliedModel>().ReverseMap();
            CreateMap<ServiceOptionDto, ServiceOptionModel>().ReverseMap();
            CreateMap<SalesSummaryDto, SalesSummaryModel>().ReverseMap();
            CreateMap<SaleDto, SaleModel>().ReverseMap();
            CreateMap<SaleCreationDto, SaleCreationModel>().ReverseMap();
            CreateMap<DebtsSummaryDto, DebtsSummaryModel>().ReverseMap();
            CreateMap<DebtDto, DebtModel>().ReverseMap();
            CreateMap<DebtCreationDto, DebtCreationModel>().ReverseMap();
            CreateMap<PayDebtDto, PayDebtModel>().ReverseMap();
            CreateMap<PayDebtsDto, PayDebtsModel>().ReverseMap();
            CreateMap<PaymentsSummaryDto, PaymentsSummaryModel>().ReverseMap();
            CreateMap<RevenueDto, RevenueModel>().ReverseMap();
            CreateMap<DashboardSummaryDto, DashboardSummaryModel>().ReverseMap();
            CreateMap<ChangePasswordDto, ChangePasswordModel>().ReverseMap();
            CreateMap<RegisterDto, RegisterModel>().ReverseMap();
            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<RolesSummaryDto, RolesSummaryModel>().ReverseMap();
            CreateMap<RoleModel, UserRoleSelectionModel>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<RoleClaimModel, RoleClaimDto>().ReverseMap();
        }
    }
}
