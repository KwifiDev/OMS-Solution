using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BL.Services.Tables
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceModel>> GetAllServicesAsync()
        {
            IEnumerable<Service> services = await _repository.GetAllAsync();

            return services?.Select(s => new ServiceModel
            {
                ServiceId = s.ServiceId,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price

            }) ?? Enumerable.Empty<ServiceModel>();
        }

        public async Task<ServiceModel?> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _repository.GetByIdAsync(serviceId);

            return service == null ? null : new ServiceModel
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price
            };
        }

        public async Task<bool> AddServiceAsync(ServiceModel model)
        {
            if (model == null) return false;

            Service service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };

            bool success = await _repository.AddAsync(service);

            if (success) model.ServiceId = service.ServiceId;

            return success;
        }

        public async Task<bool> UpdateServiceAsync(ServiceModel model)
        {
            if (model == null) return false;

            Service? service = await _repository.GetByIdAsync(model.ServiceId);

            if (service == null) return false;

            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;

            return await _repository.UpdateAsync(service);

        }

        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            if (serviceId <= 0) return false;

            return await _repository.DeleteAsync(serviceId);
        }

    }
}
