using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
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

        public async Task<IEnumerable<RevenueModel>> GetAllRevenuesAsync()
        {
            IEnumerable<Revenue> revenues = await _repository.GetAllAsync();

            return revenues?.Select(r => new RevenueModel
            {
                RevenueId = r.RevenueId,
                Amount = r.Amount,
                Notes = r.Notes,
                CreatedAt = r.CreatedAt

            }) ?? Enumerable.Empty<RevenueModel>();
        }

        public async Task<RevenueModel?> GetRevenueByIdAsync(int revenueId)
        {
            Revenue? revenue = await _repository.GetByIdAsync(revenueId);

            return revenue == null ? null : new RevenueModel
            {
                RevenueId = revenue.RevenueId,
                Amount = revenue.Amount,
                Notes = revenue.Notes,
                CreatedAt = revenue.CreatedAt
            };
        }

        public async Task<bool> AddRevenueAsync(RevenueModel model)
        {
            if (model == null) return false;

            Revenue revenue = new Revenue
            {
                Amount = model.Amount,
                Notes = model.Notes
            };

            bool success = await _repository.AddAsync(revenue);

            if (success) model.RevenueId = revenue.RevenueId;

            return success;
        }

        public async Task<bool> UpdateRevenueAsync(RevenueModel model)
        {
            if (model == null) return false;

            Revenue? revenue = await _repository.GetByIdAsync(model.RevenueId);

            if (revenue == null) return false;

            revenue.Amount = model.Amount;
            revenue.Notes = model.Notes;

            return await _repository.UpdateAsync(revenue);
        }

        public async Task<bool> DeleteRevenueAsync(int revenueId)
        {
            if (revenueId <= 0) return false;

            return await _repository.DeleteAsync(revenueId);
        }
    }
}
