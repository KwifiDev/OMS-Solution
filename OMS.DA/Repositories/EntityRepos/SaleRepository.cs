﻿using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Repositories.EntityRepos
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context)
        {

        }
    }
}
