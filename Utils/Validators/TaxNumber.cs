using System;

namespace invoice_manager.Utils.Validators
{
    public class TaxNumber : IValidator
    {
        private static readonly int[] Weights = {6, 5, 7, 2, 3, 4, 5, 6, 7};
        
        public static bool IsValid(string taxNumber)
        {
            if (taxNumber.Length != 10)
            {
                return false;
            }

            var splitTaxNumber = taxNumber.Split("");
            var numbers = Array.ConvertAll(splitTaxNumber, int.Parse);

            var sum = 0;
            for (var i = 0; i < 9; i++)
            {
                sum += Weights[i] * numbers[i];
            }

            var controlSum = sum % 11;

            return controlSum == numbers[9];
        }
    }
}