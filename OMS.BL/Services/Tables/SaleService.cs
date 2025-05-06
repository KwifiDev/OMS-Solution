using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class SaleService : GenericService<Sale, SaleModel>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(IGenericRepository<Sale> genericRepo,
                           IMapperService mapper,
                           ISaleRepository repository) : base(genericRepo, mapper)
        {
            _saleRepository = repository;
        }

    }
}
