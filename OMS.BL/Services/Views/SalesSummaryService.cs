using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class SalesSummaryService : ISalesSummaryService
    {
        private readonly ISalesSummaryRepository _repository;

        public SalesSummaryService(ISalesSummaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalesSummaryDto>> GetAllSalesSummaryAsync()
        {
            IEnumerable<SalesSummary> SalesSummaries = await _repository.GetAllAsync();

            return SalesSummaries?.Select(s => new SalesSummaryDto
            {
                SaleId = s.SaleId,
                ClientName = s.ClientName,
                ServiceName = s.ServiceName,
                Description = s.Description,
                TotalSales = s.TotalSales,
                Status = s.Status

            }) ?? Enumerable.Empty<SalesSummaryDto>();
        }

        public async Task<SalesSummaryDto?> GetSaleSummaryByIdAsync(int saleId)
        {
            SalesSummary? sale = await _repository.GetSaleSummaryByIdAsync(saleId);

            return sale == null ? null : new SalesSummaryDto
            {
                SaleId = sale.SaleId,
                ClientName = sale.ClientName,
                ServiceName = sale.ServiceName,
                Description = sale.Description,
                TotalSales = sale.TotalSales,
                Status = sale.Status
            };
        }

    }
}
