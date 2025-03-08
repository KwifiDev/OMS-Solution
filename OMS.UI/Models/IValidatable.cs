using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public interface IValidatable
    {
        bool ArePropertiesValid();
        IEnumerable<ValidationResult> GetErrors();
    }
}
