using invoice_manager.Models;  
using Microsoft.EntityFrameworkCore;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
  
namespace invoice_manager.DBContexts  
{  
    public class MainDbContext : DbContext  
    {
        public DbSet<User> Users { get; set; }  
  
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)  
        {   
        }  
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {  
            // Use Fluent API to configure  
  
            // Map entities to tables  
            modelBuilder.Entity<User>().ToTable("Users");  
  
            // Configure Primary Keys  
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");

            // Configure columns  
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();  
            modelBuilder.Entity<User>().Property(u => u.Name).HasColumnType("nvarchar(200)").IsRequired();
        }  
    }  
}  