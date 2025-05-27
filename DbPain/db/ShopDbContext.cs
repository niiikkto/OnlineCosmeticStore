using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbPain.db
{
    internal class ShopDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Seller> Sellers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=magazinpg;Username=postgres;Password=1208339");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи One-to-Many
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller) // У продукта есть одна категория
                .WithMany(c => c.Products) // У категории много продуктов
                .HasForeignKey(p => p.SellerId); // Внешний ключ

            modelBuilder.Entity<Order>()
                .HasOne(p => p.PaymentMethod) // У продукта есть одна категория
                .WithMany(c => c.Orders) // У категории много продуктов
                .HasForeignKey(p => p.PaymentMethodId); // Внешний ключ

            modelBuilder.Entity<Order>()
                .HasOne(p => p.Customer) // У продукта есть одна категория
                .WithMany(c => c.Orders) // У категории много продуктов
                .HasForeignKey(p => p.CustomerId); // Внешний ключ
        }
    }
}
