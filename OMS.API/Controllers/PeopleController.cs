using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing people data.
    /// </summary>
    [Authorize]
    [Route("api/people")]
    [ApiController]
    public class PeopleController : GenericController<IPersonService, PersonDto, PersonModel>
    {
        /// <summary>
        /// Initializes a new instance of the PeopleController class.
        /// </summary>
        /// <param name="personService">The person service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public PeopleController(IPersonService personService, IMapper mapper)
            : base(personService, mapper)
        {
        }

        #region override abstract Methods
        protected override int GetModelId(PersonModel model) => model.PersonId;
        protected override void SetDtoId(PersonDto dto, int id) => dto.PersonId = id;
        protected override async Task<PagedResult<PersonModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<PersonModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(PersonModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(PersonModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, PersonDto dto) => id == dto.PersonId;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}

