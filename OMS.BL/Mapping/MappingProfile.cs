using AutoMapper;
using OMS.BL.Dtos.Tables;
using OMS.DA.Entities;

namespace OMS.BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();

            CreateMap<Branch, BranchDto>();
            CreateMap<BranchDto, Branch>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Debt, DebtDto>();
            CreateMap<DebtDto, Debt>();

            CreateMap<Discount, DiscountDto>();
            CreateMap<DiscountDto, Discount>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();

            CreateMap<PermissionsConfig, PermissionsConfigDto>();
            CreateMap<PermissionsConfigDto, PermissionsConfig>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            CreateMap<Revenue, RevenueDto>();
            CreateMap<RevenueDto, Revenue>();

            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();

            CreateMap<Service, ServiceDto>();
            CreateMap<ServiceDto, Service>();

            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
