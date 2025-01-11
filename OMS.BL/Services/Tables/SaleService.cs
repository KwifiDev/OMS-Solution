using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;

        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SaleModel>> GetAllSalesAsync()
        {
            IEnumerable<Sale> sales = await _repository.GetAllAsync();

            return sales?.Select(s => new SaleModel
            {
                SaleId = s.SaleId,
                ClientId = s.ClientId,
                ServiceId = s.ServiceId,
                Cost = s.Cost,
                Quantity = s.Quantity,
                DiscountPercentage = s.DiscountPercentage,
                AmountDeducted = s.AmountDeducted,
                Total = s.Total,
                Description = s.Description,
                Notes = s.Notes,
                CreatedAt = s.CreatedAt,
                Status = s.Status,
                CreatedByUserId = s.CreatedByUserId

            }) ?? Enumerable.Empty<SaleModel>();
        }

        public async Task<SaleModel?> GetSaleByIdAsync(int saleId)
        {
            Sale? sale = await _repository.GetByIdAsync(saleId);

            return sale == null ? null : new SaleModel
            {
                SaleId = sale.SaleId,
                ClientId = sale.ClientId,
                ServiceId = sale.ServiceId,
                Cost = sale.Cost,
                Quantity = sale.Quantity,
                DiscountPercentage = sale.DiscountPercentage,
                AmountDeducted = sale.AmountDeducted,
                Total = sale.Total,
                Description = sale.Description,
                Notes = sale.Notes,
                CreatedAt = sale.CreatedAt,
                Status = sale.Status,
                CreatedByUserId = sale.CreatedByUserId,
            };
        }

        public async Task<bool> AddSaleAsync(SaleModel model)
        {
            if (model == null) return false;

            Sale sale = new Sale
            {
                ClientId = model.ClientId,
                ServiceId = model.ServiceId,
                Quantity = model.Quantity,
                Description = model.Description,
                Notes = model.Notes,
                Status = model.Status,
                CreatedByUserId = model.CreatedByUserId
            };

            bool success = await _repository.AddAsync(sale);

            if (success) model.SaleId = sale.SaleId;

            return success;
        }

        public async Task<bool> UpdateSaleAsync(SaleModel model)
        {
            if (model == null) return false;

            Sale? sale = await _repository.GetByIdAsync(model.SaleId);

            if (sale == null) return false;

            sale.Description = model.Description;
            sale.Notes = model.Notes;
            sale.Status = model.Status;

            return await _repository.UpdateAsync(sale);
        }

        public async Task<bool> DeleteSaleAsync(int saleId)
        {
            if (saleId <= 0) return false;

            return await _repository.DeleteAsync(saleId);
        }
    }
}
