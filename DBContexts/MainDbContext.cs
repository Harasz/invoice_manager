using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace invoice_manager.DBContexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsList> ProductsLists { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            AddDefaultChangeTracker();
        }

        private void AddDefaultChangeTracker()
        {
            ChangeTracker.StateChanged += UpdateModelDates;
        }

        private static void UpdateModelDates(object sender,
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            var entry = (IModel) e.Entry.Entity;

            if (entry == null) return;

            if (e.Entry.State is EntityState.Modified or EntityState.Added) entry.UpdatedAt = DateTime.Now;

            if (e.Entry.State is EntityState.Added) entry.CreatedAt = DateTime.Now;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Tax>().ToTable("Taxes");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductsList>().ToTable("ProductsLists");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");

            // Configure Primary Keys  
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");
            modelBuilder.Entity<Client>().HasKey(c => c.Id).HasName("PK_Clients");
            modelBuilder.Entity<Company>().HasKey(c => c.Id).HasName("PK_Companies");
            modelBuilder.Entity<Tax>().HasKey(t => t.Id).HasName("PK_Taxes");
            modelBuilder.Entity<Product>().HasKey(t => t.Id).HasName("PK_Products");
            modelBuilder.Entity<ProductsList>().HasKey(t => t.Id).HasName("PK_ProductsLists");
            modelBuilder.Entity<Invoice>().HasKey(t => t.Id).HasName("PK_Invoices");

            // Configure columns
            // User model
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnType("varchar(20)").HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnType("varchar(40)").HasMaxLength(40)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnType("varchar(100)").HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnType("char(60)").HasMaxLength(60)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion(
                    v => v.ToString(),
                    v => (UserRoles) Enum.Parse(typeof(UserRoles), v)).HasDefaultValue(UserRoles.User)
                .HasColumnType("varchar(5)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // Client model
            modelBuilder.Entity<Client>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.Name).HasColumnType("varchar(100)").HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.AddressLine1).HasColumnType("varchar(100)").HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.AddressLine2).HasColumnType("varchar(100)").HasMaxLength(100);
            modelBuilder.Entity<Client>().Property(u => u.PostalCode).HasColumnType("char(6)").HasMaxLength(6)
                .IsFixedLength().IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.City).HasColumnType("varchar(35)").HasMaxLength(35)
                .IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.TaxNumber).HasColumnType("char(10)").HasMaxLength(10)
                .IsFixedLength().IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.IBAN).HasColumnType("char(28)").HasMaxLength(28)
                .IsFixedLength();
            modelBuilder.Entity<Client>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<Client>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // Company model
            modelBuilder.Entity<Company>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.Name).HasColumnType("varchar(100)").HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.AddressLine1).HasColumnType("varchar(100)").HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.AddressLine2).HasColumnType("varchar(100)")
                .HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(u => u.PostalCode).HasColumnType("char(6)").HasMaxLength(6)
                .IsFixedLength().IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.City).HasColumnType("varchar(35)").HasMaxLength(35)
                .IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.TaxNumber).HasColumnType("char(10)").HasMaxLength(10)
                .IsFixedLength().IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.IBAN).HasColumnType("char(28)").HasMaxLength(28)
                .IsFixedLength();
            modelBuilder.Entity<Company>().Property(u => u.PhoneNumber).HasColumnType("char(9)").HasMaxLength(9)
                .IsFixedLength();
            modelBuilder.Entity<Company>().Property(u => u.Email).HasColumnType("varchar(100)").HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(u => u.Website).HasColumnType("varchar(100)").HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(u => u.LogoSourcePath).HasColumnType("char(20)").HasMaxLength(20)
                .IsFixedLength();
            modelBuilder.Entity<Company>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<Company>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // Tax model
            modelBuilder.Entity<Tax>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Tax>().Property(u => u.Multiplier).HasColumnType("float(2)").HasPrecision(2)
                .IsRequired();
            modelBuilder.Entity<Tax>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<Tax>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // Product model
            modelBuilder.Entity<Product>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.Name).HasColumnType("varchar(100)").HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.Unit).HasColumnType("varchar(5)").HasMaxLength(5)
                .IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.PricePerUnit).HasColumnType("float(2)").HasPrecision(2)
                .IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.TaxId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.OwnerId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<Product>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // ProductsList model
            modelBuilder.Entity<ProductsList>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<ProductsList>().Property(u => u.Count).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ProductsList>().Property(u => u.InvoiceId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ProductsList>().Property(u => u.ProductId).HasColumnType("int").HasPrecision(2)
                .IsRequired();
            modelBuilder.Entity<ProductsList>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<ProductsList>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // Invoice model
            modelBuilder.Entity<Invoice>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.Note).HasColumnType("varchar(255)").HasMaxLength(255);
            modelBuilder.Entity<Invoice>().Property(u => u.Type).HasConversion<string>().IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.PaymentMethod).HasConversion<string>().IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.DateDue).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.PaidAt).HasColumnType("datetime");
            modelBuilder.Entity<Invoice>().Property(u => u.IssuedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.IssuedById).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.ClientId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.CreatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            modelBuilder.Entity<Invoice>().Property(u => u.UpdatedAt).HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

            // Unique fields
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(u => u.TaxNumber).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(u => u.TaxNumber).IsUnique();
            modelBuilder.Entity<Tax>().HasIndex(u => u.Multiplier).IsUnique();

            // Configures relationship
            modelBuilder.Entity<Product>()
                .HasOne(s => s.Tax)
                .WithMany(p => p.Products)
                .HasForeignKey(s => s.TaxId);

            modelBuilder.Entity<Product>()
                .HasOne(s => s.Owner)
                .WithMany(p => p.Products)
                .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<ProductsList>()
                .HasOne(s => s.Product)
                .WithMany(p => p.ProductsLists)
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<ProductsList>()
                .HasOne(s => s.Invoice)
                .WithMany(p => p.ProductsLists)
                .HasForeignKey(s => s.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasOne(s => s.Client)
                .WithMany(p => p.Invoices)
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<Invoice>()
                .HasOne(s => s.IssuedBy)
                .WithMany(p => p.Invoices)
                .HasForeignKey(s => s.IssuedById);

            // Seed data
            modelBuilder.Entity<Tax>().HasData(Seed.Taxes.Data);
            modelBuilder.Entity<User>().HasData(Seed.Users.Data);
        }
    }
}