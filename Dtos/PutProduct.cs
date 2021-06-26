namespace invoice_manager.Dtos
{
    public class PutProduct
    {
        public string Name { get; set;}
        public string Unit { get; set;}
        public float PricePerUnit { get; set;}
        public int TaxId { get; set;}
        public int OwnerId { get; set;}
    }
}