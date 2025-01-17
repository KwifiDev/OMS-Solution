using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class DiscountService : GenericService<Discount, DiscountDto>, IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IGenericRepository<Discount> genericRepo,
                               IMapperService mapper,
                               IDiscountRepository repository) : base(genericRepo, mapper)
        {
            _discountRepository = repository;
        }


        /*
           public async Task<IEnumerable<DiscountDto>> GetAllDiscountsAsync()
        {
            IEnumerable<Discount> discounts = await _repository.GetAllAsync();

            return discounts?.Select(d => new DiscountDto
            {
                DiscountId = d.DiscountId,
                ServiceId = d.ServiceId,
                ClientType = d.ClientType,
                DiscountPercentage = d.DiscountPercentage

            }) ?? Enumerable.Empty<DiscountDto>();
        }

        public async Task<DiscountDto?> GetDiscountByIdAsync(int discountId)
        {
            Discount? discount = await _repository.GetByIdAsync(discountId);

            return discount == null ? null : new DiscountDto
            {
                DiscountId = discount.DiscountId,
                ServiceId = discount.ServiceId,
                ClientType = discount.ClientType,
                DiscountPercentage = discount.DiscountPercentage
            };
        }

        public async Task<bool> AddDiscountAsync(DiscountDto dto)
        {
            if (dto == null) return false;

            Discount discount = new Discount
            {
                ServiceId = dto.ServiceId,
                ClientType = dto.ClientType,
                DiscountPercentage = dto.DiscountPercentage
            };

            bool success = await _repository.AddAsync(discount);

            if (success) dto.DiscountId = discount.DiscountId;

            return success;
        }

        public async Task<bool> UpdateDiscountAsync(DiscountDto dto)
        {
            if (dto == null) return false;

            Discount? discount = await _repository.GetByIdAsync(dto.DiscountId);

            if (discount == null) return false;

            discount.ClientType = dto.ClientType;
            discount.DiscountPercentage = dto.DiscountPercentage;

            return await _repository.UpdateAsync(discount);

        }

        public async Task<bool> DeleteDiscountAsync(int discountId)
        {
            if (discountId <= 0) return false;

            return await _repository.DeleteAsync(discountId);
        }
         */

    }
}
