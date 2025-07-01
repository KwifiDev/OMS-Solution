namespace OMS.UI.Models
{
    public class DashboardSummaryModel : BaseModel
    {
        private decimal? _totalRevenues;
        private decimal? _deposit;
        private decimal? _withdraw;
        private decimal? _totalBalance;
        private decimal? _totalPayments;
        private decimal? _totalSales;
        private decimal? _totalSalesAmountDeducted;
        private decimal? _notPaidDebts;
        private decimal? _paidDebts;
        private decimal? _totalDebtsAmountDeducted;
        private decimal? _netCashFlow;
        private decimal? _totalIncome;
        private decimal? _outstandingDebts;


        public decimal? TotalRevenues
        {
            get => _totalRevenues;
            set => SetProperty(ref _totalRevenues, value);
        }

        public decimal? Deposit
        {
            get => _deposit;
            set => SetProperty(ref _deposit, value);
        }

        public decimal? Withdraw
        {
            get => _withdraw;
            set => SetProperty(ref _withdraw, value);
        }

        public decimal? TotalBalance
        {
            get => _totalBalance;
            set => SetProperty(ref _totalBalance, value);
        }

        public decimal? TotalPayments
        {
            get => _totalPayments;
            set => SetProperty(ref _totalPayments, value);
        }

        public decimal? TotalSales
        {
            get => _totalSales;
            set => SetProperty(ref _totalSales, value);
        }

        public decimal? TotalSalesAmountDeducted
        {
            get => _totalSalesAmountDeducted;
            set => SetProperty(ref _totalSalesAmountDeducted, value);
        }

        public decimal? NotPaidDebts
        {
            get => _notPaidDebts;
            set => SetProperty(ref _notPaidDebts, value);
        }

        public decimal? PaidDebts
        {
            get => _paidDebts;
            set => SetProperty(ref _paidDebts, value);
        }

        public decimal? TotalDebtsAmountDeducted
        {
            get => _totalDebtsAmountDeducted;
            set => SetProperty(ref _totalDebtsAmountDeducted, value);
        }

        public decimal? NetCashFlow
        {
            get => _netCashFlow;
            set => SetProperty(ref _netCashFlow, value);
        }

        public decimal? TotalIncome
        {
            get => _totalIncome;
            set => SetProperty(ref _totalIncome, value);
        }

        public decimal? OutstandingDebts
        {
            get => _outstandingDebts;
            set => SetProperty(ref _outstandingDebts, value);
        }
    }
}
