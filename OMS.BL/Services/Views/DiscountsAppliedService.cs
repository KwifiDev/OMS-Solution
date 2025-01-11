using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DiscountsAppliedService : IDiscountsAppliedService
    {
        private readonly IDiscountsAppliedRepository _repository;

        public DiscountsAppliedService(IDiscountsAppliedRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DiscountsAppliedModel>> GetAllDiscountsAppliedAsync()
        {
            IEnumerable<DiscountsApplied> discountsApplied = await _repository.GetAllAsync();

            return discountsApplied?.Select(d => new DiscountsAppliedModel
            {
                DiscountId = d.DiscountId,
                ServiceName = d.ServiceName,
                ServicePrice = d.ServicePrice,
                ClientType = d.ClientType,
                Discount = d.Discount

            }) ?? Enumerable.Empty<DiscountsAppliedModel>();
        }

        public async Task<DiscountsAppliedModel?> GetDiscountAppliedByIdAsync(int discountId)
        {
            DiscountsApplied? discount = await _repository.GetDiscountAppliedByIdAsync(discountId);

            return discount == null ? null : new DiscountsAppliedModel
            {
                DiscountId = discount.DiscountId,
                ServiceName = discount.ServiceName,
                ServicePrice = discount.ServicePrice,
                ClientType = discount.ClientType,
                Discount = discount.Discount
            };
        }

    }
}
