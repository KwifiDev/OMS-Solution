﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing users account data.
    /// </summary>
    [Route("api/usersaccount")]
    [ApiController]
    public class UsersAccountController : GenericViewController<IUserAccountService, UserAccountDto, UserAccountModel>
    {
        /// <summary>
        /// Initializes a new instance of the UsersAccountController class.
        /// </summary>
        /// <param name="userAccountService">The users account service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public UsersAccountController(IUserAccountService userAccountService, IMapper mapper)
            : base(userAccountService, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<IEnumerable<UserAccountModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<UserAccountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

