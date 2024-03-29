﻿using System;
using System.Collections.Generic;

namespace invoice_manager.Models
{
    public class Client : IModel
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string AddressLine1 { get; set;}
        public string AddressLine2 { get; set;}
        public string PostalCode { get; set;}
        public string City { get; set;}
        public string TaxNumber { get; set;}
        public string IBAN { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}