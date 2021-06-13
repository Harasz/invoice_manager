using System;
using System.Collections.Generic;

namespace invoice_manager.Models
{
    public class Tax : IModel
    {
        public int Id { get; set;}
        public float Multiplier { get; set;}
        public ICollection<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}