using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing clients summary data.
    /// </summary>
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

        /// <summary>
        /// Retrieves all clients summary from the service.
        /// </summary>
        /// <returns>A collection of ClientsSummaryModel instances.</returns>
        protected override async Task<IEnumerable<ClientsSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific clients summary by their ID.
        /// </summary>
        /// <param name="id">The ID of the clients summary to retrieve.</param>
        /// <returns>The requested ClientsSummaryModel or null if not found.</returns>
        protected override async Task<ClientsSummaryModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
    }
}

