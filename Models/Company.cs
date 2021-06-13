using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace invoice_manager.Models
{
    public class Company : IModel, IValidatableObject
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string AddressLine1 { get; set;}
        public string AddressLine2 { get; set;}
        public string PostalCode { get; set;}
        public string City { get; set;}
        public string TaxNumber { get; set;}
        public string IBAN { get; set;}
        public string PhoneNumber { get; set;}
        public string Email { get; set;}
        public string Website { get; set;}
        public string LogoSourcePath { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Product> Products { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Utils.Validators.TaxNumber.IsValid(TaxNumber))
            {
                yield return new ValidationResult(
                    "TaxNumber is not valid",
                    new[] { nameof(TaxNumber) });
            }
            
            if (!Utils.Validators.IBAN.IsValid(IBAN))
            {
                yield return new ValidationResult(
                    "IBAN is not valid",
                    new[] { nameof(IBAN) });
            }
            
            if (!Utils.Validators.PhoneNumber.IsValid(PhoneNumber))
            {
                yield return new ValidationResult(
                    "PhoneNumber is not valid",
                    new[] { nameof(PhoneNumber) });
            }
            
            if (!Utils.Validators.Email.IsValid(Email))
            {
                yield return new ValidationResult(
                    "Email is not valid",
                    new[] { nameof(Email) });
            }
        }
    }
}