using System;
using System.Collections.Generic;
using invoice_manager.Models;

namespace invoice_manager.Dtos
{
    public class GetInvoice
    {
        public int Id { get; set;}
        public string Note { get; set;}
        public InvoiceType Type { get; set;}
        public PaymentMethod PaymentMethod { get; set;}
        public DateTime DateDue { get; set;}
        public DateTime PaidAt { get; set;}
        public DateTime IssuedAt { get; set;}
        public int IssuedById { get; set;}
        public int ClientId { get; set;}
        public int CompanyId { get; set;}
        public ICollection<GetProductList> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}