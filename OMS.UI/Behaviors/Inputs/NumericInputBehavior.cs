namespace OMS.UI.Behaviors.Inputs
{
    public class NumericInputBehavior : BaseInputBehavior
    {
        protected override bool IsTextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}
