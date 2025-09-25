using OMS.BL.Interfaces;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Interfaces;
using OMS.DA.IRepositories.IViewRepos;

namespace OMS.BL.Services.Views
{
    public class GenericViewService<TEntity, TModel> : IGenericViewService<TModel>
        where TEntity : class, IEntityKey
        where TModel : class, IModelKey
    {
        private readonly IGenericViewRepository<TEntity> _repository;
        protected readonly IMapperService _mapper;

        public GenericViewService(IGenericViewRepository<TEntity> repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<PagedResult<TModel>> GetPagedAsync(PaginationParams parameters)
        {
            var pagedResult = await _repository.GetPagedAsync(parameters);

            if (pagedResult.TotalItems == 0) return new PagedResult<TModel>();

            return new PagedResult<TModel>
            {
                Items = _mapper.Map<List<TEntity>, List<TModel>>(pagedResult.Items),
                TotalItems = pagedResult.TotalItems,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }

        public virtual async Task<TModel?> GetByIdAsync(int id)
        {
            TEntity? entity = await _repository.GetByIdAsync(id);

            return entity == null ? default : _mapper.Map<TEntity, TModel>(entity);
        }
    }
}
