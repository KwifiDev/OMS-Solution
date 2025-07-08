using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.BL.Models.Hybrid;

namespace OMS.BL.Services.Tables
{
    public class DiscountService : GenericService<Discount, DiscountModel>, IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IGenericRepository<Discount> genericRepo,
                               IMapperService mapper,
                               IDiscountRepository repository) : base(genericRepo, mapper)
        {
            _discountRepository = repository;
        }

        public async Task<bool> IsDiscountAlreadyApplied(CheckDiscountAppliedModel model)
            => await _discountRepository.IsDiscountAlreadyApplied(model.ServiceId, model.ClientType);
    }
}
