using System.ComponentModel.DataAnnotations;

namespace invoice_manager.Dtos
{
    public class PutTax
    {
        [Required]
        public float? Multiplier { get; set;}
    }
}