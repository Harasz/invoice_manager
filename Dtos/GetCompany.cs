using System;

namespace invoice_manager.Dtos
{
    public class GetCompany
    {
        public int Id { get; set; }
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
    }
}