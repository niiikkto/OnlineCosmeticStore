using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbPain.db;

namespace DbPain.db_controler
{
    internal class PaymentMethodDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            foreach (var paymentMethod in context.PaymentMethods)
            {
                Console.WriteLine(
                    $"id: {paymentMethod.Id} \n" +
                    $" name: {paymentMethod.Name} \n"
                    );
            }
        }
        public static void Add(ShopDbContext context, string name)
        {
            context.PaymentMethods.Add(
                new PaymentMethod
                {
                    Name = name,
                }
            );
            context.SaveChanges();
        }
        public static void Update(ShopDbContext context, int id, string name)
        {
            var paymentMethod = context.PaymentMethods.Find(id);
            if (paymentMethod != null)
            {
                paymentMethod.Name = name;

            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var paymentMethod = context.PaymentMethods.Find(id);
            if (paymentMethod != null)
            {
                context.PaymentMethods.Remove(paymentMethod);
                context.SaveChanges();
                Console.WriteLine($"Payment method {paymentMethod.Name} deleted.");
            }
            else
            {
                Console.WriteLine("Payment method not found.");
            }
        }
    }
}
