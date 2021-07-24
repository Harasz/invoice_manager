using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace invoice_manager.Utils.Validators
{
    public class IBAN : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string iban)
            {
                return new ValidationResult("IBAN must be a string type.");
            }
            
            
            if (iban.Length != 28)
            {
                return new ValidationResult("IBAN must be 28 length long.");
            }

            if (!iban.StartsWith("PL"))
            {
                return new ValidationResult("IBAN must start with 'PL' prefix");
            }

            var accountNumber = iban["PL00".Length..];
            const string polandCountyCode = "2521";
            var controlSum = iban.Substring("PL".Length, "PL".Length);

            var convertedIban = BigInteger.Parse(accountNumber + polandCountyCode + controlSum);
            return convertedIban % 97 == 1 ? ValidationResult.Success : new ValidationResult("Control sum not match.");
        }
    }
}