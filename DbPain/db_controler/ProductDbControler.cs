using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbPain.db_controler
{
    internal class ProductDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            var products = context.Products
                .Include(p => p.Seller)
                .ToList();
            foreach (var product in context.Products)
            {
                Console.WriteLine(
                    $"id: {product.Id} \n" +
                    $" name: {product.Name} \n" +
                    $" description: {product.Description} \n" +
                    $" price: {product.Price} \n" +
                    $" quantity: {product.Quantity} \n" +
                    $" seller: {product.Seller.Name} \n\n"
                    );
            }
        }
        public static void Add(
            ShopDbContext context,
            string name,
            string description,
            double price,
            int quantity,
            int sellerId
            )
        {
            context.Products.Add(
                new Product
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Quantity = quantity,
                    SellerId = sellerId
                }
            );
            context.SaveChanges();
        }
        public static void Update(
            ShopDbContext context,
            int id,
            string name,
            string description,
            double price,
            int quantity,
            int sellerId
            )
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.Quantity = quantity;
                product.SellerId = sellerId;
            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                Console.WriteLine($"Product {product.Name} deleted.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
    }
}
