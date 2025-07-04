﻿namespace OMS.BL.Models.Views
{
    public class DashboardSummaryModel
    {
        public decimal TotalRevenues { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdraw { get; set; }

        public decimal TotalBalance { get; set; }

        public decimal TotalPayments { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalSalesAmountDeducted { get; set; }

        public decimal NotPaidDebts { get; set; }

        public decimal PaidDebts { get; set; }

        public decimal TotalDebtsAmountDeducted { get; set; }

        public decimal NetCashFlow { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
