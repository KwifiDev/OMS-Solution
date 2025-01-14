using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class ServiceService : GenericService<Service, ServiceDto>, IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IGenericRepository<Service> repo,
                              IMapperService mapper,
                              IServiceRepository repository) : base(repo, mapper)
        {
            _serviceRepository = repository;
        }

        /*
                 public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            IEnumerable<Service> services = await _serviceRepository.GetAllAsync();

            return services?.Select(s => new ServiceDto
            {
                ServiceId = s.ServiceId,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price

            }) ?? Enumerable.Empty<ServiceDto>();
        }

        public async Task<ServiceDto?> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId);

            return service == null ? null : new ServiceDto
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price
            };
        }

        public async Task<bool> AddServiceAsync(ServiceDto dto)
        {
            if (dto == null) return false;

            Service service = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            bool success = await _serviceRepository.AddAsync(service);

            if (success) dto.ServiceId = service.ServiceId;

            return success;
        }

        public async Task<bool> UpdateServiceAsync(ServiceDto dto)
        {
            if (dto == null) return false;

            Service? service = await _serviceRepository.GetByIdAsync(dto.ServiceId);

            if (service == null) return false;

            service.Name = dto.Name;
            service.Description = dto.Description;
            service.Price = dto.Price;

            return await _serviceRepository.UpdateAsync(service);

        }

        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            if (serviceId <= 0) return false;

            return await _serviceRepository.DeleteAsync(serviceId);
        }
         */

    }
}
