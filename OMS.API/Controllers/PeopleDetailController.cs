using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing People Detail data.
    /// </summary>
    [Route("api/peopledetail")]
    [ApiController]
    public class PeopleDetailController : GenericViewController<IPersonDetailService, PersonDetailDto, PersonDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the People Detail Controller class.
        /// </summary>
        /// <param name="personDetailService">The People detail service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public PeopleDetailController(IPersonDetailService personDetailService, IMapper mapper)
            : base(personDetailService, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<IEnumerable<PersonDetailModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<PersonDetailModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}

