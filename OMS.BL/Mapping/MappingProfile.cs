using AutoMapper;
using OMS.BL.Dtos.Tables;
using OMS.DA.Entities;

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

        }
    }
}
