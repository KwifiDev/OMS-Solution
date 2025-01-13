using AutoMapper;
using OMS.BL.IServices.Tables;
using OMS.DA.IRepositories.IEntityRepos;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OMS.BL.Services.Tables
{
    public class GenericService<TDto, TEntity> : IGenericService<TDto> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
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

        public async Task<TDto?> GetByIdAsync(int id)
        {
            TEntity? entity = await _repository.GetByIdAsync(id);

            return entity == null ? default(TDto) : _mapper.Map<TDto>(entity);
        }

        public async Task<bool> AddAsync(TDto dto)
        {
            if (dto == null) return false;

            TEntity entity = _mapper.Map<TEntity>(dto);

            bool success = await _repository.AddAsync(entity);

            if (success) SetNewPrimaryKey(dto, entity);

            return success;
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            int primaryKey = GetPrimaryKey(dto);

            TEntity? existingEntity = await _repository.GetByIdAsync(primaryKey);

            if (existingEntity == null) return false;

            _mapper.Map(dto, existingEntity);

            return await _repository.UpdateAsync(existingEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            return await _repository.DeleteAsync(id);
        }


        // Code encapsulation
        private int GetPrimaryKey(TDto dto)
        {
            if (dto == null) return -1;

            var keyProperty = typeof(TDto).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)))
                ?? throw new ArgumentException("No primary key property found");

            var primaryKey = keyProperty.GetValue(dto);

            if (primaryKey == null) return -1;

            return Convert.ToInt32(primaryKey);
        }
        private void SetNewPrimaryKey(TDto dto, TEntity entity)
        {
            PropertyInfo? keyProperty = typeof(TDto).GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty != null)
            {
                PropertyInfo? entityKeyProperty = typeof(TEntity).GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

                if (entityKeyProperty != null)
                {
                    object? newId = entityKeyProperty.GetValue(entity);
                    keyProperty.SetValue(dto, newId);
                }
            }
        }

    }
}
