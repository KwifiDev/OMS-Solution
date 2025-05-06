using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
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

    }
}
