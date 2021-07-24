using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace invoice_manager.Utils.Validators
{
    public class TaxNumber : ValidationAttribute
    {
        private static readonly uint[] Weights = {6, 5, 7, 2, 3, 4, 5, 6, 7};
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string taxNumber)
            {
                return new ValidationResult("Tax number must be a string type.");
            }
            
            if (taxNumber.Length != 10)
            {
                return new ValidationResult("Tax number must be 10 length long.");
            }

            var splitTaxNumber = taxNumber.ToCharArray().Select(c => c.ToString()).ToArray();
            var numbers = Array.ConvertAll(splitTaxNumber, BigInteger.Parse);

            BigInteger sum = 0;
            for (var i = 0; i < 9; i++)
            {
                sum += Weights[i] * numbers[i];
            }

            var controlSum = sum % 11;

            return controlSum == numbers[9] ? ValidationResult.Success : new ValidationResult("Tax number control sum not match.");
        }
    }
}