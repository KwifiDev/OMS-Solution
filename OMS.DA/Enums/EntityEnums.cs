namespace OMS.DA.Enums
{
    public enum EnClientType : byte
    {
        Normal,
        Lawyer,
        Other
    }

    public enum EnGender // Convert This enum To Bool DataType Using Fluent API
    {
        Male,
        Female
    }

    public enum EnSaleStatus : byte
    {
        Uncompleted,
        Completed,
        Canceled
    }

    public enum EnDebtStatus : byte
    {
        NotPaid,
        Paid,
        Canceled
    }

    public enum EnTransactionType : byte
    {
        Deposit,
        Withdraw,
        Transfer
    }

    public enum EnAccountTransactionStatus
    {
        Empty = -2,
        Failed = -1,
        Success = 0,
        AccountNotFound = 1,
        InsufficientBalance = 2
    }

    public enum EnPayDebtStatus
    {
        Empty = -2,
        Failed = -1,
        Success = 0,
        DebtNotFound = 1,
        AccountNotFound = 2,
        InsufficientBalance = 3
    }

}
