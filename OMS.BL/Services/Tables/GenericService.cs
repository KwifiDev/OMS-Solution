using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OMS.BL.Services.Tables
{
    public class GenericService<TEntity, TDto> : IGenericService<TDto> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapperService _mapperService;

        public GenericService(IGenericRepository<TEntity> repository, IMapperService mapper)
        {
            _repository = repository;
            _mapperService = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await _repository.GetAllAsync();

            if (entities == null)
            {
                return Enumerable.Empty<TDto>();
            }

            return _mapperService.Map<TEntity, TDto>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            TEntity? entity = await _repository.GetByIdAsync(id);

            return entity == null ? default(TDto) : _mapperService.Map<TEntity, TDto>(entity);
        }

        public virtual async Task<bool> AddAsync(TDto dto)
        {
            if (dto == null) return false;

            TEntity entity = _mapperService.Map<TDto, TEntity>(dto);

            bool success = await _repository.AddAsync(entity);

            if (success) SetNewPrimaryKey(dto, entity);

            return success;
        }

        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            int primaryKey = GetPrimaryKey(dto);

            TEntity? existingEntity = await _repository.GetByIdAsync(primaryKey);

            if (existingEntity == null) return false;

            _mapperService.Map(dto, existingEntity);

            return await _repository.UpdateAsync(existingEntity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            return await _repository.DeleteAsync(id);
        }


        // Code encapsulation
        private int GetPrimaryKey(TDto dto)
        {
            if (dto == null) return -1;

            PropertyInfo? keyProperty = typeof(TDto).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty == null) throw new ArgumentException("No primary key property found");

            object? primaryKey = keyProperty.GetValue(dto);

            return primaryKey == null ? -1 : Convert.ToInt32(primaryKey);
        }

        private void SetNewPrimaryKey(TDto dto, TEntity entity)
        {
            PropertyInfo? dtoKeyProperty = typeof(TDto).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (dtoKeyProperty == null) return;

            PropertyInfo? entityKeyProperty = typeof(TEntity).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (entityKeyProperty == null) return;

            object? newId = entityKeyProperty.GetValue(entity);
            dtoKeyProperty.SetValue(dto, newId);
        }


    }
}
