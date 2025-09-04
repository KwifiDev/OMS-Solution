namespace OMS.UI.Models.Others
{
    public class DebtCreationModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        public short Quantity { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
