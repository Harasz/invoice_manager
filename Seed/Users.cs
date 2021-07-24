using System.Collections.Generic;
using invoice_manager.Models;

namespace invoice_manager.Seed
{
    public class Users
    {
        public static IEnumerable<User> Data = new List<User>
        {
            new()
            {
                Id = 1, FirstName = "Admin", LastName = "System", Email = "admin@domain.com",
                PasswordHash = "spmmLRXaJaiLCrZQ7q4esKP8lQqswKy4Lsu6jdBvqvB9ox9G", Role = UserRoles.Admin
            }
        };
    }
}