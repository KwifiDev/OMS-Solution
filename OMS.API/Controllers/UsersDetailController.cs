﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing Users Detail data.
    /// </summary>
    [Route("api/usersdetail")]
    [ApiController]
    public class UsersDetailController : GenericViewController<IUserDetailService, UserDetailDto, UserDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the UsersDetailController class.
        /// </summary>
        /// <param name="userDetailService">The user detail service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public UsersDetailController(IUserDetailService userDetailService, IMapper mapper)
            : base(userDetailService, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<IEnumerable<UserDetailModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<UserDetailModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

