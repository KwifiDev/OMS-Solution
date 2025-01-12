using OMS.BL.IServices.Tables;
using OMS.BL.Dtos.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class RevenueService : IRevenueService
    {
        private readonly IRevenueRepository _repository;

        public RevenueService(IRevenueRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RevenueDto>> GetAllRevenuesAsync()
        {
            IEnumerable<Revenue> revenues = await _repository.GetAllAsync();

            return revenues?.Select(r => new RevenueDto
            {
                RevenueId = r.RevenueId,
                Amount = r.Amount,
                Notes = r.Notes,
                CreatedAt = r.CreatedAt

            }) ?? Enumerable.Empty<RevenueDto>();
        }

        public async Task<RevenueDto?> GetRevenueByIdAsync(int revenueId)
        {
            Revenue? revenue = await _repository.GetByIdAsync(revenueId);

            return revenue == null ? null : new RevenueDto
            {
                RevenueId = revenue.RevenueId,
                Amount = revenue.Amount,
                Notes = revenue.Notes,
                CreatedAt = revenue.CreatedAt
            };
        }

        public async Task<bool> AddRevenueAsync(RevenueDto dto)
        {
            if (dto == null) return false;

            Revenue revenue = new Revenue
            {
                Amount = dto.Amount,
                Notes = dto.Notes
            };

            bool success = await _repository.AddAsync(revenue);

            if (success) dto.RevenueId = revenue.RevenueId;

            return success;
        }

        public async Task<bool> UpdateRevenueAsync(RevenueDto dto)
        {
            if (dto == null) return false;

            Revenue? revenue = await _repository.GetByIdAsync(dto.RevenueId);

            if (revenue == null) return false;

            revenue.Amount = dto.Amount;
            revenue.Notes = dto.Notes;

            return await _repository.UpdateAsync(revenue);
        }

        public async Task<bool> DeleteRevenueAsync(int revenueId)
        {
            if (revenueId <= 0) return false;

            return await _repository.DeleteAsync(revenueId);
        }
    }
}
