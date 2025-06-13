﻿using AutoMapper;
using OMS.UI.APIs.Dtos.Hybrid;
using OMS.UI.APIs.Dtos.StoredProcedureParams;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.Dtos.Views;
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
            CreateMap<CreateSaleDto, CreateSaleModel>().ReverseMap();
        }
    }
}
