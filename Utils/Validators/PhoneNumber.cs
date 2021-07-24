using System.ComponentModel.DataAnnotations;

namespace invoice_manager.Utils.Validators
{
    public class PhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string phoneNumber)
            {
                return new ValidationResult("Email must be a string type.");
            }

            if (phoneNumber.Length != 9)
            {
                return new ValidationResult("Phone number must be 9 length long.");
            }

            if (!int.TryParse(phoneNumber, out _))
            {
                return new ValidationResult("Phone number accept only digits.");
            }
            
            return ValidationResult.Success;
        }
    }
}