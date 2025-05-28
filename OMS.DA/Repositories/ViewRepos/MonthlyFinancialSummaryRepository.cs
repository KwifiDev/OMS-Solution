using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class MonthlyFinancialSummaryRepository : GenericViewRepository<MonthlyFinancialSummary>, IMonthlyFinancialSummaryRepository
    {
        public MonthlyFinancialSummaryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
