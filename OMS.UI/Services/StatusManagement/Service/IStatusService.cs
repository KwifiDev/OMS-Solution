﻿namespace OMS.UI.Services.StatusManagement.Service
{
    public interface IStatusService
    {
        AddEditStatus CreateAddEditStatus();
        SearchStatus CreateSearchStatus();
        TransactionStatus CreateTransactionStatus();
        DebtStatus CreateDebtStatus();
    }
}
