#nullable enable
using System.ComponentModel.DataAnnotations;

namespace invoice_manager.Dtos
{
    public class PutProduct
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set;}
        [Required]
        [StringLength(5)]
        public string? Unit { get; set;}
        [Required]
        public float? PricePerUnit { get; set;}
        [Required]
        public int? TaxId { get; set;}
        [Required]
        public int? OwnerId { get; set;}
    }
}