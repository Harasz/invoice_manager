#nullable enable
using System.ComponentModel.DataAnnotations;
using invoice_manager.Utils.Validators;

namespace invoice_manager.Dtos
{
    public class PutClient
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set;}
        [Required]
        [StringLength(100)]
        public string? AddressLine1 { get; set;}
        [StringLength(100)]
        public string? AddressLine2 { get; set;}
        [Required]
        [StringLength(6)]
        public string? PostalCode { get; set;}
        [Required]
        [StringLength(35)]
        public string? City { get; set;}
        [Required]
        [StringLength(10)]
        [TaxNumber]
        public string? TaxNumber { get; set;}
        [Required]
        [StringLength(28)]
        [IBAN]
        public string? IBAN { get; set;}
    }
}