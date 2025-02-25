using AutoMapper;
using OMS.BL.Dtos.Tables;
using OMS.UI.Models;

namespace OMS.UI.Mapping
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<PersonDto, PersonModel>().ReverseMap();
            CreateMap<BranchDto, BranchModel>().ReverseMap();
        }
    }
}
