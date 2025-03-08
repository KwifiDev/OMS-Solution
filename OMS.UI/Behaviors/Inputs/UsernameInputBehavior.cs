using System.Text.RegularExpressions;

namespace OMS.UI.Behaviors.Inputs
{
    public class UsernameInputBehavior : BaseInputBehavior
    {
        protected override bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]+$");
            return regex.IsMatch(text);
        }
    }
}
