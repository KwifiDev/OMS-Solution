using System.Text.RegularExpressions;

namespace OMS.UI.Behaviors.Inputs
{
    public class ArabicAndNumbersInputBehavior : BaseInputBehavior
    {
        protected override bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[\u0600-\u06FF0-9\-]+$");
            return regex.IsMatch(text);
        }
    }
}
