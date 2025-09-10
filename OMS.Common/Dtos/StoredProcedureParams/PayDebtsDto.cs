using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.StoredProcedureParams
{
    public class PayDebtsDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Client ID must be a positive number")]
        public int ClientId { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Notes Length must be between (5 - 100)")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "User Id required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
        public int CreatedByUserId { get; set; }

        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
