using System;
using System.Collections.Generic;

namespace invoice_manager.Models
{
    public enum InvoiceType
    {
        Vat,
        Simplified,
        VatMargin,
        ProForm,
        Advance,
        Final,
        Corrective,
    }
    
    public enum PaymentMethod
    {
        Cash,
        Transfer,
        Card,
        Ci,
    }
    
    public class Invoice : IModel
    {
        public int Id { get; set;}
        public string Note { get; set;}
        public InvoiceType Type { get; set;}
        public PaymentMethod PaymentMethod { get; set;}
        public DateTime DateDue { get; set;}
        public DateTime PaidAt { get; set;}
        public DateTime IssuedAt { get; set;}
        public User IssuedBy { get; set;}
        public int IssuedById { get; set;}
        public Client Client { get; set;}
        public int ClientId { get; set;}
        public ICollection<ProductsList> ProductsLists { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}