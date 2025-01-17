using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Views
{
    public class GenericViewService<TEntity, TDto> : IGenericViewService<TDto> where TEntity : class
    {
        private readonly IGenericViewRepository<TEntity> _repository;
        private readonly IMapperService _mapper;

        public GenericViewService(IGenericViewRepository<TEntity> repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _repository.GetAllAsync();

            if (entities == null)
            {
                return Enumerable.Empty<TDto>();
            }

            return _mapper.Map<TEntity, TDto>(entities);
        }

        public async Task<TDto?> GetByIdAsync(int id)
        {
            TEntity? entity = await _repository.GetByIdAsync(id);

            return entity == null ? default(TDto) : _mapper.Map<TEntity, TDto>(entity);
        }
    }
}
