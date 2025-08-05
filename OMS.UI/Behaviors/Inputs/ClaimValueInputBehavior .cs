using System.Text.RegularExpressions;

namespace OMS.UI.Behaviors.Inputs
{
    public class ClaimValueInputBehavior : BaseInputBehavior
    {
        protected override bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[a-zA-Z.]*$");
            return regex.IsMatch(text);
        }
    }
}
