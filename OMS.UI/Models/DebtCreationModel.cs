namespace OMS.UI.Models
{
    public class DebtCreationModel
    {
        public int DebtId { get; set; }

        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        public short Quantity { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
