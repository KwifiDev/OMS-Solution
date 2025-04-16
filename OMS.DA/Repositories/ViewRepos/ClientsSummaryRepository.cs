﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientsSummaryRepository : GenericViewRepository<ClientsSummary>, IClientsSummaryRepository
    {
        private readonly DbSet<ClientsSummary> _ClientsSummaries;

        public ClientsSummaryRepository(AppDbContext context) : base(context)
        {
            _ClientsSummaries = context.Set<ClientsSummary>();
        }

    }
}
