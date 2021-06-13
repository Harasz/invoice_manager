using System.Text.RegularExpressions;

namespace invoice_manager.Utils.Validators
{
    public class Email : IValidator
    {
        public static bool IsValid(string email)
        {
            var regex = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$", RegexOptions.IgnoreCase);
            var match = regex.Match(email);
            return match.Success;
        }
    }
}