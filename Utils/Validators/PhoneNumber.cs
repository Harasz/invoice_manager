namespace invoice_manager.Utils.Validators
{
    public class PhoneNumber : IValidator
    {
        public static bool IsValid(string phoneNumber)
        {
            return phoneNumber.Length == 9 && int.TryParse(phoneNumber, out _);
        }
    }
}