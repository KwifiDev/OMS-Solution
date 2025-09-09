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
    /// API controller for managing Branch Operational Metric data.
    /// </summary>
    [Authorize]
    [Route("api/branchesoperationalmetric")]
    [ApiController]
    public class BranchesOperationalMetricController : GenericViewController<IBranchOperationalMetricService, BranchOperationalMetricDto, BranchOperationalMetricModel>
    {
        /// <summary>
        /// Initializes a new instance of the Branch Operational Metric Controller class.
        /// </summary>
        /// <param name="branchOperationalMetricService">The Branch Operational Metric service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public BranchesOperationalMetricController(IBranchOperationalMetricService branchOperationalMetricService, IMapper mapper)
            : base(branchOperationalMetricService, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<PagedResult<BranchOperationalMetricModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<BranchOperationalMetricModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

