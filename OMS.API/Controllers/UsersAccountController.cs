using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.Common.Dtos.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing users account data.
    /// </summary>
    [Authorize]
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
        protected override async Task<PagedResult<UserAccountModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<UserAccountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

