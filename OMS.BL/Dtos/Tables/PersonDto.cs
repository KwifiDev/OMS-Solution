using OMS.DA.Enums;

namespace OMS.BL.Dtos.Tables;

public partial class PersonDto
{
    public int PersonId { get; internal set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    /// <summary>
    /// 0 = Male | 1 = Female
    /// </summary>
    public required EnGender Gender { get; set; }

    public string? Phone { get; set; }

    // ========================================================================
    public string GenderText { get => Gender == EnGender.Male ? "ذكر" : "انثى"; }
    public string FullName { get => $"{FirstName} {LastName}"; }


}
