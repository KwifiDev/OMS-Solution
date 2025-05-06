using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class TransactionsByTypeService : GenericViewService<TransactionsByType, TransactionsByTypeModel>, ITransactionsByTypeService
    {
        private readonly ITransactionsByTypeRepository _transactionsByTypeRepository;

        public TransactionsByTypeService(IGenericViewRepository<TransactionsByType> genericRepo,
                                         IMapperService mapper,
                                         ITransactionsByTypeRepository repository) : base(genericRepo, mapper)
        {
            _transactionsByTypeRepository = repository;
        }

    }
}
