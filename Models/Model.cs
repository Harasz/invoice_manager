using System;

namespace invoice_manager.Models
{
    public interface IModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }  
    }
}