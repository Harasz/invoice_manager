using System;
using System.Collections.Generic;

namespace invoice_manager.Models
{
    public enum UserRoles
    {
        Admin,
        User
    }
    public class User : IModel
    {
        public int Id { get; set;}  
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string Email { get; set;}
        public string PasswordHash { get; set;}
        public UserRoles Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public ICollection<Invoice> Invoices { get; set; }
    }
}