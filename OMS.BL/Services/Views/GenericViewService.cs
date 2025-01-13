using AutoMapper;
using OMS.BL.IServices.Views;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Views
{
    public class GenericViewService<TDto, TEntity> : IGenericViewService<TDto> where TEntity : class
    {
        private readonly IGenericViewRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericViewService(IGenericViewRepository<TEntity> repository, IMapper mapper)
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

            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        //public async Task<TDto?> GetByIdAsync(int id)
        //{
        //    TEntity? entity = await _repository.GetByIdAsync(id);

        //    return entity == null ? default(TDto) : _mapper.Map<TDto>(entity);
        //}
    }
}
