using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class TransactionsByTypeService : ITransactionsByTypeService
    {
        private readonly ITransactionsByTypeRepository _transactionsByTypeRepository;
        private readonly IMapperService _mapper;

        public TransactionsByTypeService(ITransactionsByTypeRepository repository, IMapperService mapper)
        {
            _transactionsByTypeRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionsByTypeModel>> GetAllAsync()
        {
            var data = await _transactionsByTypeRepository.GetAllAsync();
            return data != null ? _mapper.Map<TransactionsByType, TransactionsByTypeModel>(data) : Enumerable.Empty<TransactionsByTypeModel>();
        }
    }
}
