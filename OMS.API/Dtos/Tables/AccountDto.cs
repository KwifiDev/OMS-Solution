using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;


public partial class AccountDto
{
    [Key]
    public int Id { get; set; }

    public required int ClientId { get; set; }

    [Required(ErrorMessage = "UserAccount is required")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "UserAccount must be between 3 and 20 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "UserAccount can only contain letters, numbers and underscores")]
    public required string UserAccount { get; set; }

    public decimal Balance { get; set; } = 0;
}

