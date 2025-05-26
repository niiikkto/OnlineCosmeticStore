using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbPain.db_controler
{
    internal class AdminDbControler : DbControler
    {
        public static void Read(ShopDbContext context)
        {
            foreach (var admin in context.Admins)
            {
                Console.WriteLine(
                    $"id: {admin.Id} \n" +
                    $" name: {admin.Name} \n" +
                    $" email: {admin.Email} \n" +
                    $" right: {admin.Rights} \n\n"
                    );
            }
        }
        public static void Add(ShopDbContext context, string name, string email, RightsList right)
        {
            context.Admins.Add(
                new Admin { 
                    Name = name,
                    Email = email,
                    Rights = right.ToString()
                }
            );
            context.SaveChanges();
        }
        public static void Update(ShopDbContext context, int id, string name, string email, RightsList right)
        {
            var admin = context.Admins.Find(id);
            if (admin != null)
            {
                admin.Name = name;
                admin.Email = email;
                admin.Rights = right.ToString();
            }
            context.SaveChanges();
        }
        public static void Delete(ShopDbContext context, int id)
        {
            var admin = context.Admins.Find(id);
            if (admin != null)
            {
                context.Admins.Remove(admin);
                context.SaveChanges();
                Console.WriteLine($"Admin {admin.Name} deleted.");
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }
    }
}
