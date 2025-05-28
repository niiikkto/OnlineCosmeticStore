using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbPain.db;

namespace DbPain.db_controler
{
    internal class OrderDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            var orders = context.Orders
                .Include(p => p.Customer)
                .ToList();
            orders = context.Orders
                .Include(p => p.Product)
                .ToList();
            orders = context.Orders
                .Include(p => p.PaymentMethod)
                .ToList();
            foreach (var order in context.Orders)
            {
                Console.WriteLine(
                    $"id: {order.Id} \n" +
                    $" customer: {order.Customer.Name} \n" +
                    $" date of creation: {order.DateOfCreation} \n" +
                    $" quantity: {order.Quantity} \n" +
                    $" product: {order.Product.Name} \n" +
                    $" payment method: {order.PaymentMethod.Name} \n" +
                    $" status: {order.Status} \n" +
                    $" price All: {order.PriceAll} \n\n"
                    );
            }
        }
        public static void Add(
            ShopDbContext context,
            int customerId,
            string dateOfCreation,
            int quantity,
            int productId,
            int paymentMethodId,
            string status,
            double priceAll
            )
        {
            context.Orders.Add(
                new Order
                {
                    CustomerId = customerId,
                    DateOfCreation = dateOfCreation,
                    Quantity = quantity,
                    ProductId = productId,
                    PaymentMethodId = paymentMethodId,
                    Status = status,
                    PriceAll = priceAll

                }
            );
            context.SaveChanges();
        }
        public static void Update(
            ShopDbContext context,
            int id,
            int customerId,
            string dateOfCreation,
            int quantity,
            int productId,
            int paymentMethodId,
            string status,
            double priceAll
            )
        {
            var order = context.Orders.Find(id);
            if (order != null)
            {
                order.CustomerId = customerId;
                order.DateOfCreation = dateOfCreation;
                order.Quantity = quantity;
                order.ProductId = productId;
                order.PaymentMethodId = paymentMethodId;
                order.Status = status;
                order.PriceAll = priceAll;
            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var order = context.Orders.Find(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
                Console.WriteLine($"Order {order.Id} deleted.");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }
    }
}
