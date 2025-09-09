using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.StoredProcedureParams
{
    public class PayDebtsDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Client ID must be a positive number")]
        public int ClientId { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "User Id required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
        public int CreatedByUserId { get; set; }

        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
