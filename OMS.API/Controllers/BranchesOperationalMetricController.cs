using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing Branch Operational Metric data.
    /// </summary>
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

        /// <summary>
        /// Retrieves all BranchesOperationalMetric from the service.
        /// </summary>
        /// <returns>A collection of BranchOperationalMetric Model instances.</returns>
        protected override async Task<IEnumerable<BranchOperationalMetricModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific BranchOperationalMetric by their ID.
        /// </summary>
        /// <param name="id">The ID of the BranchOperationalMetric to retrieve.</param>
        /// <returns>The requested BranchOperationalMetric Model or null if not found.</returns>
        protected override async Task<BranchOperationalMetricModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
    }
}

