using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Views
{
    public class GenericViewService<TEntity, TModel> : IGenericViewService<TModel> where TEntity : class
    {
        private readonly IGenericViewRepository<TEntity> _repository;
        protected readonly IMapperService _mapper;

        public GenericViewService(IGenericViewRepository<TEntity> repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _repository.GetAllAsync();

            if (entities == null)
            {
                return Enumerable.Empty<TModel>();
            }

            return _mapper.Map<TEntity, TModel>(entities);
        }

        public async Task<TModel?> GetByIdAsync(int id)
        {
            TEntity? entity = await _repository.GetByIdAsync(id);

            return entity == null ? default(TModel) : _mapper.Map<TEntity, TModel>(entity);
        }
    }
}
