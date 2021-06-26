using System;
using invoice_manager.Models;

namespace invoice_manager.Dtos
{
    public class GetProduct
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string Unit { get; set;}
        public float PricePerUnit { get; set;}
        public Tax Tax { get; set;}
        public int TaxId { get; set;}
        public int OwnerId { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}