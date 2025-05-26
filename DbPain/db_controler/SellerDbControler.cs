using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DbPain.db_controler
{
    internal class SellerDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            foreach (var seller in context.Sellers)
            {
                Console.WriteLine(
                    $"id: {seller.Id} \n" +
                    $" name: {seller.Name} \n" +
                    $" number phone: {seller.NumberPhone} \n" +
                    $" email: {seller.Email} \n\n"
                    );
            }
        }
        public static void Add(ShopDbContext context, string name, string numberPhone, string email)
        {
            context.Sellers.Add(
                new Seller
                {
                    Name = name,
                    NumberPhone = numberPhone,
                    Email = email,
                    
                }
            );
            context.SaveChanges();
        }
        public static void Update(ShopDbContext context, int id, string name, string numberPhone, string email)
        {
            var seller = context.Sellers.Find(id);
            if (seller != null)
            {
                seller.Name = name;
                seller.NumberPhone = numberPhone;
                seller.Email = email;
            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var seller = context.Sellers.Find(id);
            if (seller != null)
            {
                context.Sellers.Remove(seller);
                context.SaveChanges();
                Console.WriteLine($"Seller {seller.Name} deleted.");
            }
            else
            {
                Console.WriteLine("Seller not found.");
            }
        }   
    }
}
