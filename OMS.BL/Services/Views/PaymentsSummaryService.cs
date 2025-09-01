using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class PaymentsSummaryService : GenericViewService<PaymentsSummary, PaymentsSummaryModel>, IPaymentsSummaryService
    {
        private readonly IPaymentsSummaryRepository _paymentsSummaryRepository;

        public PaymentsSummaryService(IGenericViewRepository<PaymentsSummary> genericRepo,
                                      IMapperService mapper,
                                      IPaymentsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _paymentsSummaryRepository = repository;
        }

        public async Task<PagedResult<PaymentsSummaryModel>> GetPaymentsByAccountIdPagedAsync(int accountId, PaginationParams parameters)
        {
            var pagedResult = await _paymentsSummaryRepository.GetPaymentsByAccountIdPagedAsync(accountId, parameters);

            return new PagedResult<PaymentsSummaryModel>
            {
                Items = _mapper.Map<List<PaymentsSummary>, List<PaymentsSummaryModel>>(pagedResult.Items),
                TotalItems = pagedResult.TotalItems,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
    }
}
