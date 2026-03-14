using Microsoft.EntityFrameworkCore;
using ShopKart.API.Models.Entities;

namespace ShopKart.API.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor - recevies configurations from DI
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets - each represents a table in DB
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }

        // Configure entity relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //      CATEGORY CONFIGURATION
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Description)
                      .HasMaxLength(500);
            });

            //      PRODUCT CONFIGURATION
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Description)
                      .HasMaxLength(1000);

                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(p => p.ImageUrl)
                      .HasMaxLength(500);

                // Relationship: Product belongs to Category
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //      CUSTOMER CONFIGURATION
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(c => c.Email)
                      .IsUnique();

                entity.Property(c => c.Phone)
                      .HasMaxLength(20);

                entity.Property(c => c.Address)
                      .HasMaxLength(500);
            });

            //      ORDER CONFIGURATION
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.OrderNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(o => o.OrderNumber)
                      .IsUnique();

                entity.Property(o => o.TotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(o => o.Status)
                      .IsRequired()
                      .HasMaxLength(50);

                // Relationship Order belongs to Customer
                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //      ORDER ITEM CONFIGURATION
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.UnitPrice)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(oi => oi.TotalPrice)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                // Relationship: OrderItem belongs to Order
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: OrderItem belongs to Product
                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //      SEED DATA
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices", CreatedAt = fixedDate, IsActive = true },
                new Category { Id = 2, Name = "Clothing", Description = "Men and Women clothing", CreatedAt = fixedDate, IsActive = true },
                new Category { Id = 3, Name = "Books", Description = "Physical and digital books", CreatedAt = fixedDate, IsActive = true }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "iPhone 15 Pro", Description = "Latest Apple smartphone", Price = 134900.00m, Stock = 50, ImageUrl = "/images/iphone15.jpg", CategoryId = 1, CreatedAt = fixedDate, IsActive = true },
                new Product { Id = 2, Name = "Samsung Galaxy S24", Description = "Samsung flagship", Price = 79999.00m, Stock = 75, ImageUrl = "/images/s24.jpg", CategoryId = 1, CreatedAt = fixedDate, IsActive = true },
                new Product { Id = 3, Name = "Sony WH-1000XM5", Description = "Premium headphones", Price = 29990.00m, Stock = 100, ImageUrl = "/images/sony-xm5.jpg", CategoryId = 1, CreatedAt = fixedDate, IsActive = true },
                new Product { Id = 4, Name = "Levis 501 Jeans", Description = "Classic jeans", Price = 3499.00m, Stock = 200, ImageUrl = "/images/levis501.jpg", CategoryId = 2, CreatedAt = fixedDate, IsActive = true },
                new Product { Id = 5, Name = "Clean Code Book", Description = "Robert C. Martin book", Price = 499.00m, Stock = 150, ImageUrl = "/images/cleancode.jpg", CategoryId = 3, CreatedAt = fixedDate, IsActive = true }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Rahul", LastName = "Sharma", Email = "rahul.sharma@email.com", Phone = "9876543210", Address = "123, MG Road, Delhi", CreatedAt = fixedDate, IsActive = true },
                new Customer { Id = 2, FirstName = "Priya", LastName = "Patel", Email = "priya.patel@email.com", Phone = "8765432109", Address = "456, Park Street, Mumbai", CreatedAt = fixedDate, IsActive = true }
            );
        }
    }
}
