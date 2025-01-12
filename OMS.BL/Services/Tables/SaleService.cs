using OMS.BL.IServices.Tables;
using OMS.BL.Dtos.Tables;
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

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            IEnumerable<Sale> sales = await _repository.GetAllAsync();

            return sales?.Select(s => new SaleDto
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

            }) ?? Enumerable.Empty<SaleDto>();
        }

        public async Task<SaleDto?> GetSaleByIdAsync(int saleId)
        {
            Sale? sale = await _repository.GetByIdAsync(saleId);

            return sale == null ? null : new SaleDto
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

        public async Task<bool> AddSaleAsync(SaleDto dto)
        {
            if (dto == null) return false;

            Sale sale = new Sale
            {
                ClientId = dto.ClientId,
                ServiceId = dto.ServiceId,
                Quantity = dto.Quantity,
                Description = dto.Description,
                Notes = dto.Notes,
                Status = dto.Status,
                CreatedByUserId = dto.CreatedByUserId
            };

            bool success = await _repository.AddAsync(sale);

            if (success) dto.SaleId = sale.SaleId;

            return success;
        }

        public async Task<bool> UpdateSaleAsync(SaleDto dto)
        {
            if (dto == null) return false;

            Sale? sale = await _repository.GetByIdAsync(dto.SaleId);

            if (sale == null) return false;

            sale.Description = dto.Description;
            sale.Notes = dto.Notes;
            sale.Status = dto.Status;

            return await _repository.UpdateAsync(sale);
        }

        public async Task<bool> DeleteSaleAsync(int saleId)
        {
            if (saleId <= 0) return false;

            return await _repository.DeleteAsync(saleId);
        }
    }
}
