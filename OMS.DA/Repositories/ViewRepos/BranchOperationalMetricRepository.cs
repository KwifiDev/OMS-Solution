﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class BranchOperationalMetricRepository : GenericViewRepository<BranchOperationalMetric>, IBranchOperationalMetricRepository
    {
        private readonly DbSet<BranchOperationalMetric> _branchOperationalMetric;

        public BranchOperationalMetricRepository(AppDbContext context) : base(context)
        {
            _branchOperationalMetric = context.Set<BranchOperationalMetric>();
        }
    }
}
