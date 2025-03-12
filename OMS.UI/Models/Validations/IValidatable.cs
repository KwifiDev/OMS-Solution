using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Validations
{
    public interface IValidatable
    {
        bool ArePropertiesValid();
        IEnumerable<ValidationResult> GetErrors();
    }
}
