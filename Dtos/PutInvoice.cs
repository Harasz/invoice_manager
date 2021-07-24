#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using invoice_manager.Models;
namespace invoice_manager.Dtos
{
    public class PutInvoice
    {
        [StringLength(255)]
        public string? Note { get; set;}
        [Required]
        [EnumDataType(typeof(InvoiceType))]
        public InvoiceType? Type { get; set;}
        [Required]
        [EnumDataType(typeof(PaymentMethod))]
        public PaymentMethod? PaymentMethod { get; set;}
        [Required]
        public DateTime? DateDue { get; set;}
        public DateTime? PaidAt { get; set;}
        [Required]
        public int? ClientId { get; set;}
        public PutProductList[]? Products { get; set;}
    }
}