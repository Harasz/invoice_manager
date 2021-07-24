#nullable enable
using System.ComponentModel.DataAnnotations;

namespace invoice_manager.Dtos
{
    public class PutProductList
    {
        [Required]
        public int? Count { get; set;}
        [Required]
        public int? ProductId { get; set;}
    }
}