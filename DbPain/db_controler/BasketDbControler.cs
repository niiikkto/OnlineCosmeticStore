using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbPain.db_controler
{
    internal class BasketDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            var baskets = context.Baskets
                .Include(p => p.Product)
                .ToList();
            foreach (var basket in context.Baskets)
            {
                Console.WriteLine(
                    $"id: {basket.Id} \n" +
                    $"product: {basket.Product.Name} \n\n"
                    );
            }
        }
        public static void Add(ShopDbContext context, int productId)
        {
            context.Baskets.Add(
                new Basket
                {
                    ProductId = productId
                }
            );
            context.SaveChanges();
        }
        public static void Update(ShopDbContext context, int id, int productId)
        {
            var basket = context.Baskets.Find(id);
            if (basket != null)
            {
                basket.ProductId = productId;
            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var basket = context.Baskets.Find(id);
            if (basket != null)
            {
                context.Baskets.Remove(basket);
                context.SaveChanges();
                Console.WriteLine($"Product id {basket.ProductId} deleted.");
            }
            else
            {
                Console.WriteLine("Product id not found.");
            }
        }
    }
}
