﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class UserAccountRepository : GenericViewRepository<UserAccount>, IUserAccountRepository
    {
        private readonly DbSet<UserAccount> _userAccounts;

        public UserAccountRepository(AppDbContext context) : base(context)
        {
            _userAccounts = context.Set<UserAccount>();
        }

    }
}
