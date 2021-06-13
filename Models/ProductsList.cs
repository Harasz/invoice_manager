using System;

namespace invoice_manager.Models
{
    public class ProductsList : IModel
    {
        public int Id { get; set;}
        public int Count { get; set;}
        public int InvoiceId { get; set;}
        public Invoice Invoice { get; set;}
        public int ProductId { get; set;}
        public Product Product { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}