using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientsByTypeService : IClientsByTypeService
    {
        private readonly IClientsByTypeRepository _clientsByTypeRepository;
        private readonly IMapperService _mapper;

        public ClientsByTypeService(IClientsByTypeRepository repository, IMapperService mapper)
        {
            _clientsByTypeRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientsByTypeModel>> GetAllAsync()
        {
            var data = await _clientsByTypeRepository.GetAllAsync();
            return data != null ? _mapper.Map<ClientsByType, ClientsByTypeModel>(data) : Enumerable.Empty<ClientsByTypeModel>();
        }
    }
}
