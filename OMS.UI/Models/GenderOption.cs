using OMS.Common.Enums;
using System.Collections.ObjectModel;

namespace OMS.UI.Models
{
    public class GenderOption
    {
        public string DisplayMember { get; set; } = null!;
        public EnGender Value { get; set; }

        public static ObservableCollection<GenderOption> Genders { get; private set; } = null!;

        static GenderOption()
        {
            Genders =
            [
                new() { DisplayMember = "ذكر", Value = EnGender.Male },
                new() { DisplayMember = "انثى", Value = EnGender.Female },
            ];
        }
    }
}
