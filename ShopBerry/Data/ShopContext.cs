using Microsoft.EntityFrameworkCore;
using ShopBerry.Models;

namespace ShopBerry.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Начальные данные
            modelBuilder.Entity<Shop>().HasData(
                new Shop { Id = 1, Name = "Shop A", Address = "Address A" },
                new Shop { Id = 2, Name = "Shop B", Address = "Address B" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Customer A", Address = "Customer Address A" },
                new Customer { Id = 2, Name = "Customer B", Address = "Customer Address B" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product A", Price = 10.0M, Color = "Red", ShopId = 1 },
                new Product { Id = 2, Name = "Product B", Price = 20.0M, Color = "Blue", ShopId = 2 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, IsCompleted = false , CustomerId = 1 },
                new Order { Id = 2, IsCompleted = true , CustomerId = 2 }
            );
        }
    }
}
