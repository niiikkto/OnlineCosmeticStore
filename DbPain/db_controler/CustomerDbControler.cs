using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbPain.db_controler
{
    internal class CustomerDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            foreach (var customer in context.Customers)
            {
                Console.WriteLine(
                    $"id: {customer.Id} \n" +
                    $" basket id: {customer.BasketId} \n" +
                    $" name: {customer.Name} \n" +
                    $" number phone: {customer.NumberPhone} \n" +
                    $" delivery address: {customer.DeliveryAddress} \n" +
                    $" email: {customer.Email} \n\n"
                    );
            }
        }
        public static void Add(
            ShopDbContext context,
            int basketId,
            string name,
            string numberPhone,
            string deliveryAddress,
            string email
            )
        {
            context.Customers.Add(
                new Customer
                {
                    BasketId = basketId,
                    Name = name,
                    NumberPhone = numberPhone,
                    DeliveryAddress = deliveryAddress,
                    Email = email
                }
            );
            context.SaveChanges();
        }
        public static void Update(
            ShopDbContext context,
            int id,
            int basketId,
            string name,
            string numberPhone,
            string deliveryAddress,
            string email
            )
        {
            var customer = context.Customers.Find(id);
            if (customer != null)
            {
                customer.BasketId = basketId;
                customer.Name = name;
                customer.NumberPhone = numberPhone;
                customer.DeliveryAddress = deliveryAddress;
                customer.Email = email;
            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var customer = context.Customers.Find(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
                Console.WriteLine($"Customer {customer.Name} deleted.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }
}
