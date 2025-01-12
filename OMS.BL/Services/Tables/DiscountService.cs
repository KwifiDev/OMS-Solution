using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DiscountModel>> GetAllDiscountsAsync()
        {
            IEnumerable<Discount> discounts = await _repository.GetAllAsync();

            return discounts?.Select(d => new DiscountModel
            {
                DiscountId = d.DiscountId,
                ServiceId = d.ServiceId,
                ClientType = d.ClientType,
                DiscountPercentage = d.DiscountPercentage

            }) ?? Enumerable.Empty<DiscountModel>();
        }

        public async Task<DiscountModel?> GetDiscountByIdAsync(int discountId)
        {
            Discount? discount = await _repository.GetByIdAsync(discountId);

            return discount == null ? null : new DiscountModel
            {
                DiscountId = discount.DiscountId,
                ServiceId = discount.ServiceId,
                ClientType = discount.ClientType,
                DiscountPercentage = discount.DiscountPercentage
            };
        }

        public async Task<bool> AddDiscountAsync(DiscountModel model)
        {
            if (model == null) return false;

            Discount discount = new Discount
            {
                ServiceId = model.ServiceId,
                ClientType = model.ClientType,
                DiscountPercentage = model.DiscountPercentage
            };

            bool success = await _repository.AddAsync(discount);

            if (success) model.DiscountId = discount.DiscountId;

            return success;
        }

        public async Task<bool> UpdateDiscountAsync(DiscountModel model)
        {
            if (model == null) return false;

            Discount? discount = await _repository.GetByIdAsync(model.DiscountId);

            if (discount == null) return false;

            discount.ClientType = model.ClientType;
            discount.DiscountPercentage = model.DiscountPercentage;

            return await _repository.UpdateAsync(discount);

        }

        public async Task<bool> DeleteDiscountAsync(int discountId)
        {
            if (discountId <= 0) return false;

            return await _repository.DeleteAsync(discountId);
        }

    }
}
