﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class DiscountsAppliedRepository : GenericViewRepository<DiscountsApplied>, IDiscountsAppliedRepository
    {
        private readonly DbSet<DiscountsApplied> _discountsApplieds;

        public DiscountsAppliedRepository(AppDbContext context) : base(context)
        {
            _discountsApplieds = context.Set<DiscountsApplied>();
        }

    }
}
