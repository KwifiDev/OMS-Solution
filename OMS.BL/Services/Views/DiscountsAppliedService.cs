using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DiscountsAppliedService : GenericViewService<DiscountsApplied, DiscountsAppliedDto>, IDiscountsAppliedService
    {
        private readonly IDiscountsAppliedRepository _discountsAppliedRepository;

        public DiscountsAppliedService(IGenericViewRepository<DiscountsApplied> genericRepo,
                                       IMapperService mapper,
                                       IDiscountsAppliedRepository repository) : base(genericRepo, mapper)
        {
            _discountsAppliedRepository = repository;
        }



        /*
                 public async Task<IEnumerable<DiscountsAppliedDto>> GetAllDiscountsAppliedAsync()
        {
            IEnumerable<DiscountsApplied> discountsApplied = await _repository.GetAllAsync();

            return discountsApplied?.Select(d => new DiscountsAppliedDto
            {
                DiscountId = d.DiscountId,
                ServiceName = d.ServiceName,
                ServicePrice = d.ServicePrice,
                ClientType = d.ClientType,
                Discount = d.Discount

            }) ?? Enumerable.Empty<DiscountsAppliedDto>();
        }

        public async Task<DiscountsAppliedDto?> GetDiscountAppliedByIdAsync(int discountId)
        {
            DiscountsApplied? discount = await _repository.GetByIdAsync(discountId);

            return discount == null ? null : new DiscountsAppliedDto
            {
                DiscountId = discount.DiscountId,
                ServiceName = discount.ServiceName,
                ServicePrice = discount.ServicePrice,
                ClientType = discount.ClientType,
                Discount = discount.Discount
            };
        }
         */

    }
}
