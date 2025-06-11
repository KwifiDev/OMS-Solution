using OMS.Common.Enums;

namespace OMS.API.Dtos.StoredProcedureParams
{
    public class CreateSaleDto
    {
        public int SaleId { get; set; }

        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        public short Quantity { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public EnSaleStatus Status { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
