using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class TransactionsByTypeService : GenericViewService<TransactionsByType, TransactionsByTypeDto>, ITransactionsByTypeService
    {
        private readonly ITransactionsByTypeRepository _transactionsByTypeRepository;

        public TransactionsByTypeService(IGenericViewRepository<TransactionsByType> genericRepo,
                                         IMapperService mapper,
                                         ITransactionsByTypeRepository repository) : base(genericRepo, mapper)
        {
            _transactionsByTypeRepository = repository;
        }


        /*
         
        public async Task<IEnumerable<TransactionsByTypeDto>> GetAllTransactionsByTypeAsync()
        {
            IEnumerable<TransactionsByType> transactionsByType = await _repository.GetAllAsync();

            return transactionsByType?.Select(t => new TransactionsByTypeDto
            {
               TransactionType = t.TransactionType,
               TotalTransactions = t.TotalTransactions,
               TotalAmount = t.TotalAmount

            }) ?? Enumerable.Empty<TransactionsByTypeDto>();
        }
         */

    }
}
