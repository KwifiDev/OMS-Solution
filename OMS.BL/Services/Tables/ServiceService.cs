﻿using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Tables;
using OMS.BL.Models.Views;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class ServiceService : GenericService<Service, ServiceModel>, IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IGenericRepository<Service> genericRepo,
                              IMapperService mapper,
                              IServiceRepository repository) : base(genericRepo, mapper)
        {
            _serviceRepository = repository;
        }

        public async Task<IEnumerable<ServiceOptionModel>?> GetAllServicesOption()
        {
            var services = await _serviceRepository.GetAllServicesOption();

            return services == null ? null : _mapperService.Map<Service, ServiceOptionModel>(services);
        }
    }
}
