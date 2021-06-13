using System;
using System.Collections.Generic;

namespace invoice_manager.Models
{
    public class Product : IModel
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string Unit { get; set;}
        public float PricePerUnit { get; set;}
        public Tax Tax { get; set;}
        public int TaxId { get; set;}
        public Company Owner { get; set;}
        public int OwnerId { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ProductsList> ProductsLists { get; set; }
    }
}