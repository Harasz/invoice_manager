using System;
using invoice_manager.Models;

namespace invoice_manager.Dtos
{
    public class GetProductList
    {
        public int Id { get; set;}
        public int Count { get; set;}
        public int InvoiceId { get; set;}
        public GetProduct Product { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}