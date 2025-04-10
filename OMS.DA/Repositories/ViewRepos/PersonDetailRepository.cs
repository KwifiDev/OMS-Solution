﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class PersonDetailRepository : GenericViewRepository<PersonDetail>, IPersonDetailRepository
    {
        private readonly DbSet<PersonDetail> _personDetails;

        public PersonDetailRepository(AppDbContext context) : base(context)
        {
            _personDetails = context.Set<PersonDetail>();
        }
    }
}
