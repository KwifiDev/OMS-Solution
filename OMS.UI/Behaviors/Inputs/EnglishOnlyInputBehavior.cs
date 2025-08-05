using System.Text.RegularExpressions;

namespace OMS.UI.Behaviors.Inputs
{
    public class EnglishOnlyInputBehavior : BaseInputBehavior
    {
        protected override bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(text);
        }
    }
}
