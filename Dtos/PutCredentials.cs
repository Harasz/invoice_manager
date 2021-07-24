#nullable enable
using System.ComponentModel.DataAnnotations;
using invoice_manager.Utils.Validators;

namespace invoice_manager.Dtos
{
    public class PutCredentials
    {
        [Required]
        [Email]
        public string? Email { get; set;}
        [Required]
        public string? Password { get; set;}
    }
}