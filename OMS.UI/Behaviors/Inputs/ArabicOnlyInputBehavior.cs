using System.Text.RegularExpressions;

namespace OMS.UI.Behaviors.Inputs
{
    public class ArabicOnlyInputBehavior : BaseInputBehavior
    {
        protected override bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[\u0600-\u06FF]+$");
            return regex.IsMatch(text);
        }
    }
}
