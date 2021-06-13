using System.Collections.Generic;
using invoice_manager.Models;

namespace invoice_manager.Seed
{
    public class Users
    {
        public static IEnumerable<User> Data = new List<User>
        {
            new() {Id = 1, FirstName = "Admin", LastName = "System", Email = "admin@domain.com", PasswordHash="0xc"}
        };
    }
}