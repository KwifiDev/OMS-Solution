﻿using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientsSummaryService : GenericViewService<ClientsSummary, ClientsSummaryModel>, IClientsSummaryService
    {
        private readonly IClientsSummaryRepository _branchClientsSummaryRepository;

        public ClientsSummaryService(IGenericViewRepository<ClientsSummary> genericRepo,
                                     IMapperService mapper,
                                     IClientsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _branchClientsSummaryRepository = repository;
        }
    }
}
