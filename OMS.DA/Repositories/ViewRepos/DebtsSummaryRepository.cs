﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class DebtsSummaryRepository : GenericViewRepository<DebtsSummary>, IDebtsSummaryRepository
    {
        private readonly DbSet<DebtsSummary> _debtsSummaries;

        public DebtsSummaryRepository(AppDbContext context) : base(context)
        {
            _debtsSummaries = context.Set<DebtsSummary>();
        }

    }
}
