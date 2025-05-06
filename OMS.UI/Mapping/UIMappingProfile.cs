using AutoMapper;
using OMS.UI.APIServices.Dtos.Hybrid;
using OMS.UI.APIServices.Dtos.StoredProcedureParams;
using OMS.UI.APIServices.Dtos.Tables;
using OMS.UI.APIServices.Dtos.Views;
using OMS.UI.Models;


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
            CreateMap<BranchOptionDto, BranchOption>().ReverseMap();
            CreateMap<PersonDetailDto, PersonDetailModel>().ReverseMap();
            CreateMap<BranchOperationalMetricDto, BranchOperationalMetricModel>().ReverseMap();
            CreateMap<UserLoginDto, UserLoginModel>().ReverseMap();
            CreateMap<ClientDto, ClientModel>().ReverseMap();
            CreateMap<ClientsSummaryDto, ClientsSummaryModel>().ReverseMap();
            CreateMap<AccountDto, AccountModel>().ReverseMap();
            CreateMap<UserAccountDto, UserAccountModel>().ReverseMap();
            CreateMap<AccountTransactionDto, AccountTransactionModel>().ReverseMap();
        }
    }
}
