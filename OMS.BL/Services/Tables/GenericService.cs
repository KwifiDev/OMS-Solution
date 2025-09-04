using OMS.BL.Interfaces;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Interfaces;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class GenericService<TEntity, TModel> : IGenericService<TModel>
        where TEntity : class, IEntityKey
        where TModel : class, IModelKey
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapperService _mapperService;

        public GenericService(IGenericRepository<TEntity> repository, IMapperService mapper)
        {
            _repository = repository;
            _mapperService = mapper;
        }

        public virtual async Task<PagedResult<TModel>> GetPagedAsync(PaginationParams parameters)
        {
            var pagedResult = await _repository.GetPagedAsync(parameters);

            if (pagedResult.TotalItems == 0) return new PagedResult<TModel>();

            return new PagedResult<TModel>
            {
                Items = _mapperService.Map<List<TEntity>, List<TModel>>(pagedResult.Items),
                TotalItems = pagedResult.TotalItems,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }

        public virtual async Task<TModel?> GetByIdAsync(int id)
        {
            if (id <= 0) return default;

            TEntity? entity = await _repository.GetByIdAsync(id);

            return entity == null ? default : _mapperService.Map<TEntity, TModel>(entity);
        }

        public virtual async Task<bool> IsExistAsync(int id)
        {
            return await _repository.IsExistAsync(id);
        }

        public virtual async Task<bool> AddAsync(TModel model)
        {
            if (model == null) return false;

            TEntity entity = _mapperService.Map<TModel, TEntity>(model);

            bool success = await _repository.AddAsync(entity);

            if (success) model.Id = entity.Id;

            return success;
        }

        public virtual async Task<bool> UpdateAsync(TModel model)
        {
            if (model == null) return false;

            TEntity? existingEntity = await _repository.GetByIdAsync(model.Id);

            if (existingEntity == null) return false;

            _mapperService.Map(model, existingEntity);

            return await _repository.UpdateAsync(existingEntity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            return await _repository.DeleteAsync(id);
        }


        //// Code encapsulation
        //protected int GetPrimaryKey(TModel model)
        //{
        //    PropertyInfo? keyProperty = typeof(TModel).GetProperties()
        //        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

        //    if (keyProperty == null) throw new ArgumentException("No primary key property found");

        //    object? primaryKey = keyProperty.GetValue(model);

        //    return primaryKey == null ? -1 : Convert.ToInt32(primaryKey);
        //}

        //protected void SetNewPrimaryKey(TModel model, TEntity entity)
        //{
        //    PropertyInfo? modelKeyProperty = typeof(TModel).GetProperties()
        //        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

        //    if (modelKeyProperty == null) return;

        //    PropertyInfo? entityKeyProperty = typeof(TEntity).GetProperties()
        //        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

        //    if (entityKeyProperty == null) return;

        //    object? newId = entityKeyProperty.GetValue(entity);
        //    modelKeyProperty.SetValue(model, newId);
        //}


    }
}
