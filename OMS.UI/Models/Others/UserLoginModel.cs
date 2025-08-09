using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Others
{
    public class UserLoginModel
    {
        [Key]
        public int UserId { get; internal set; }

        public int PersonId { get; set; }

        public int BranchId { get; internal set; }

        public string Username { get; set; } = null!;

        public int Permissions { get; set; }

        public bool IsActive { get; set; } = false;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public EnGender Gender { get; set; }

        public TokenModel TokenInfo { get; set; } = null!;

        // Display Props
        public string FullName { get => FirstName + " " + LastName; }
    }
}
