using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.StoredProcedureParams
{
    public class DebtCreationDto
    {
        public int DebtId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Client Id must positive number")]
        public int ClientId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Service Id must positive number")]
        public int ServiceId { get; set; }

        [Range(1, 100, ErrorMessage = "Quantity must be between (1 - 100)")]
        public short Quantity { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description Length must be between (5 - 100)")]
        public string? Description { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Notes Length must be between (5 - 100)")]
        public string? Notes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CreatedByUserId must positive number")]
        public int CreatedByUserId { get; set; }
    }
}
