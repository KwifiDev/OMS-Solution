﻿using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAllBranchesOption();
    }
}
