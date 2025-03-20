﻿using AutoMapper;
using OMS.BL.Dtos.Hybrid;
using OMS.BL.Dtos.Tables;
using OMS.BL.Dtos.Views;
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
        }
    }
}
