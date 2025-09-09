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
    /// API controller for managing clients summary data.
    /// </summary>
    [Authorize]
    [Route("api/clientssummary")]
    [ApiController]
    public class ClientsSummaryController : GenericViewController<IClientsSummaryService, ClientsSummaryDto, ClientsSummaryModel>
    {
        /// <summary>
        /// Initializes a new instance of the clients summary Controller class.
        /// </summary>
        /// <param name="personDetailService">The clients summary service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public ClientsSummaryController(IClientsSummaryService clientsSummaryService, IMapper mapper)
            : base(clientsSummaryService, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<PagedResult<ClientsSummaryModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<ClientsSummaryModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

