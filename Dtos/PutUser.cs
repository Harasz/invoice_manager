#nullable enable
using System.ComponentModel.DataAnnotations;
using invoice_manager.Utils.Validators;

namespace invoice_manager.Dtos
{
    public class PutUser
    {
        [Required]
        [StringLength(20)]
        public string? FirstName { get; set;}
        [Required]
        [StringLength(40)]
        public string? LastName { get; set;}
        [Required]
        [Email]
        public string? Email { get; set;}
        [Required]
        public string? Password { get; set;}
    }
}