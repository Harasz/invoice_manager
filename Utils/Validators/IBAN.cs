namespace invoice_manager.Utils.Validators
{
    public class IBAN : IValidator
    {
        public static bool IsValid(string iban)
        {
            if (iban.Length != 28)
            {
                return false;
            }

            if (!iban.StartsWith("PL"))
            {
                return false;
            }

            var accountNumber = iban["PL00".Length..];
            const string polandCountyCode = "2521";
            var controlSum = iban.Substring("PL".Length, "PL000".Length);

            var convertedIban = int.Parse(accountNumber + polandCountyCode + controlSum);
            return convertedIban % 97 == 1;
        }
    }
}