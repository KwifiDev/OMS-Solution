using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views
{
    [Keyless]
    public class DashboardSummary
    {
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalRevenues { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Deposit { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Withdraw { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalBalance { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalPayments { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalSales { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalSalesAmountDeducted { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? NotPaidDebts { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? PaidDebts { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalDebtsAmountDeducted { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? NetCashFlow { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalIncome { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal? OutstandingDebts { get; set; }
    }
}
