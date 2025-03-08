using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public partial class BaseModel : ObservableValidator, IValidatable
    {
        public bool ArePropertiesValid()
        {
            ValidateAllProperties();
            return !HasErrors;
        }

        public IEnumerable<ValidationResult> GetErrors()
        {
            return base.GetErrors();
        }
    }
}
