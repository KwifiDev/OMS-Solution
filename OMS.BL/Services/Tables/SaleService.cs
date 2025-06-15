using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.BL.Models.StoredProcedureParams;
using OMS.Common.Enums;

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

        public async Task<bool> AddSaleAsync(CreateSaleModel model)
        {
            model.SaleId = await _saleRepository.AddSaleAsync
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

        public async Task<bool> CancelSaleAsync(int saleId)
        {
            var saleModel = await GetByIdAsync(saleId);

            if (saleModel == null) return false;

            if (saleModel.Status == EnSaleStatus.Completed || saleModel.Status == EnSaleStatus.Canceled) return false;

            saleModel.Status = EnSaleStatus.Canceled;

            return await UpdateAsync(saleModel);
        }
    }
}
