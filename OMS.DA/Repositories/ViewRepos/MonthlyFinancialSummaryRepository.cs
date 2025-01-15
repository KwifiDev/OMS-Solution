using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class MonthlyFinancialSummaryRepository : GenericViewRepository<MonthlyFinancialSummary>, IMonthlyFinancialSummaryRepository
    {
        public MonthlyFinancialSummaryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
