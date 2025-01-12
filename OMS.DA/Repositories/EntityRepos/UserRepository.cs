﻿using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
    }
}
