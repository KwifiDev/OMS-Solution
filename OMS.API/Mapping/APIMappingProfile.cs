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

            CreateMap<PermissionsConfigModel, PermissionsConfigDto>().ReverseMap();

            CreateMap<PersonModel, PersonDto>().ReverseMap();

            CreateMap<RevenueModel, RevenueDto>().ReverseMap();

            CreateMap<SaleModel, SaleDto>().ReverseMap();

            CreateMap<ServiceModel, ServiceDto>().ReverseMap();

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

        }
    }
}
