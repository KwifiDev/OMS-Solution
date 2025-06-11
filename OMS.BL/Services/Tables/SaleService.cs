using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.BL.Models.StoredProcedureParams;

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

        public async Task<bool> CreateNewSaleAsync(CreateSaleModel model)
        {
            model.SaleId = await _saleRepository.CreateNewSaleAsync
                (
                    clientId: model.ClientId,
                    serviceId: model.ServiceId,
                    quantity: model.Quantity,
                    description: model.Description,
                    notes: model.Notes,
                    status: model.Status,
                    createdByUserId: model.CreatedByUserId
                );

            return model.SaleId > 0;
        }
    }
}
