using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace invoice_manager.Utils.Validators
{
    public class Email : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string email)
            {
                return new ValidationResult("Email must be a string type.");
            }
            
            var regex = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$", RegexOptions.IgnoreCase);
            var match = regex.Match(email);
            
            return match.Success ? ValidationResult.Success : new ValidationResult("Email is not valid");
        }
    }
}