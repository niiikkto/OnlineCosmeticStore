using DbPain.db_controler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Reflection.Emit;

public class Admin
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Rights { get; set; }

}

public class PaymentMethod
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}

public class Seller
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NumberPhone { get; set; }
    public string Email { get; set; }
    public List<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; }
}


public class Basket
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public string Name { get; set; }
    public string NumberPhone { get; set; }
    public string DeliveryAddress { get; set; }
    public string Email { get; set; }
    public Basket Basket { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string DateOfCreation { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int PaymentMethodId { get; set; }
    public string Status { get; set; }
    public double PriceAll { get; set; }
    public Customer Customer { get; set; }
    public Product Product { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}

public enum RightsList
{
    ProductManageMent,
    OrderManageMent,
    FullAccess,
}

public enum Status
{
    Issued,
    OnTheWay,
    Delivered,
    Cancelled,
}


public class ShopDbContext : DbContext
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

class Program
{
    public static void GenerateTestData(ShopDbContext context)
    {
        // Очистка базы данных перед генерацией новых данных
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Генерация администраторов
        var admins = new[]
        {
            new Admin { Name = "Иван Иванов", Email = "admin1@example.com", Rights = RightsList.FullAccess.ToString() },
            new Admin { Name = "Петр Петров", Email = "admin2@example.com", Rights = RightsList.ProductManageMent.ToString() },
            new Admin { Name = "Сергей Сергеев", Email = "admin3@example.com", Rights = RightsList.OrderManageMent.ToString() }
        };
        context.Admins.AddRange(admins);

        // Генерация способов оплаты
        var paymentMethods = new[]
        {
            new PaymentMethod { Name = "Наличные" },
            new PaymentMethod { Name = "Кредитная карта" },
            new PaymentMethod { Name = "Банковский перевод" },
            new PaymentMethod { Name = "Электронный кошелек" }
        };
        context.PaymentMethods.AddRange(paymentMethods);

        // Генерация продавцов
        var sellers = new[]
        {
            new Seller { Name = "ООО ТехноМир", NumberPhone = "+79001234567", Email = "tech@example.com" },
            new Seller { Name = "ИП Смирнов", NumberPhone = "+79007654321", Email = "smirnov@example.com" },
            new Seller { Name = "ООО Электросила", NumberPhone = "+79009876543", Email = "electro@example.com" }
        };
        context.Sellers.AddRange(sellers);
        context.SaveChanges(); // Сохраняем, чтобы получить ID

        // Генерация продуктов
        var products = new[]
        {
            new Product { Name = "Смартфон X10", Description = "Новый флагманский смартфон", Price = 59990, Quantity = 50, SellerId = sellers[0].Id },
            new Product { Name = "Ноутбук ProBook", Description = "Мощный ноутбук для работы", Price = 89990, Quantity = 30, SellerId = sellers[0].Id },
            new Product { Name = "Наушники Wireless", Description = "Беспроводные наушники с шумоподавлением", Price = 12990, Quantity = 100, SellerId = sellers[1].Id },
            new Product { Name = "Умные часы V2", Description = "Фитнес-трекер с пульсометром", Price = 8990, Quantity = 75, SellerId = sellers[1].Id },
            new Product { Name = "Планшет MediaPad", Description = "8-дюймовый планшет", Price = 24990, Quantity = 40, SellerId = sellers[2].Id },
            new Product { Name = "Электронная книга", Description = "Читалка с E-Ink экраном", Price = 10990, Quantity = 60, SellerId = sellers[2].Id }
        };
        context.Products.AddRange(products);
        context.SaveChanges();

        // Генерация корзин
        var baskets = new[]
        {
            new Basket { ProductId = products[0].Id },
            new Basket { ProductId = products[1].Id },
            new Basket { ProductId = products[2].Id },
            new Basket { ProductId = products[3].Id },
            new Basket { ProductId = products[4].Id }
        };
        context.Baskets.AddRange(baskets);
        context.SaveChanges();

        // Генерация покупателей
        var customers = new[]
        {
            new Customer { Name = "Алексей Алексеев", NumberPhone = "+79161234567", DeliveryAddress = "ул. Ленина, д.10, кв.5", Email = "alex@example.com", BasketId = baskets[0].Id },
            new Customer { Name = "Дмитрий Дмитриев", NumberPhone = "+79167654321", DeliveryAddress = "ул. Пушкина, д.15, кв.12", Email = "dmitry@example.com", BasketId = baskets[1].Id },
            new Customer { Name = "Елена Еленова", NumberPhone = "+79169876543", DeliveryAddress = "пр. Мира, д.20, кв.7", Email = "elena@example.com", BasketId = baskets[2].Id }
        };
        context.Customers.AddRange(customers);
        context.SaveChanges();

        // Генерация заказов
        var random = new Random();
        var orders = new[]
        {
            new Order {
                CustomerId = customers[0].Id,
                DateOfCreation = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"),
                Quantity = 1,
                ProductId = products[0].Id,
                PaymentMethodId = paymentMethods[1].Id,
                Status = Status.Delivered.ToString(),
                PriceAll = products[0].Price
            },
            new Order {
                CustomerId = customers[0].Id,
                DateOfCreation = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"),
                Quantity = 2,
                ProductId = products[2].Id,
                PaymentMethodId = paymentMethods[2].Id,
                Status = Status.OnTheWay.ToString(),
                PriceAll = products[2].Price * 2
            },
            new Order {
                CustomerId = customers[1].Id,
                DateOfCreation = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"),
                Quantity = 1,
                ProductId = products[1].Id,
                PaymentMethodId = paymentMethods[0].Id,
                Status = Status.Issued.ToString(),
                PriceAll = products[1].Price
            },
            new Order {
                CustomerId = customers[2].Id,
                DateOfCreation = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                Quantity = 3,
                ProductId = products[3].Id,
                PaymentMethodId = paymentMethods[3].Id,
                Status = Status.Cancelled.ToString(),
                PriceAll = products[3].Price * 3
            }
        };
        context.Orders.AddRange(orders);

        // Сохранение всех изменений
        context.SaveChanges();

        Console.WriteLine("Тестовые данные успешно сгенерированы!");
    }
    

    public static int ConsoleRights(int role, string name)
    {
        switch (role)
        {
            case 1:
                if (true)
                {
                    return 1;
                }
                break;
            case 2:
                if (true)
                {
                    return 2;
                }
                break;
            case 3:
                if (true)
                {
                    return 3;
                }
                break;
        }
        return 0;
    }




    public static void ConsoleAdmin()
    {

    }

    public static void ConsoleSeller()
    {
        Console.WriteLine("Выбертье действие за поставшика:");
        Console.WriteLine("1 - Добавить товар на склад");
        Console.WriteLine("2 - Убрать товар со склада");
        Console.WriteLine("3 - Узнать сови данные");
        Console.WriteLine("4 - Поменять данные совего товара");
        int chose = int.Parse(Console.ReadLine());
        switch (chose)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public static void ConsoleCustomer()
    {
        Console.WriteLine("Выбертье действие за пользователя:");
        Console.WriteLine("1 - Добавить товар в корзину");
        Console.WriteLine("2 - Убрать товар с корзины");
        Console.WriteLine("3 - Узнать сови данные");
        Console.WriteLine("4 - Оформить заказать товар");
        Console.WriteLine("5 - Отменить заказ");
        int chose = int.Parse(Console.ReadLine());
        switch (chose)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }


    public static void Main()
    {
        var context = new ShopDbContext();
        context.Database.EnsureCreated();
        //GenerateTestData(context);
        OrderDbControler.Read(context);
        //int isValidRole = 0;
        //int role;
        //string name;
        //Console.WriteLine("Выберите роль:");
        //Console.WriteLine("1 - Админ");
        //Console.WriteLine("2 - Пользователь");
        //Console.WriteLine("3 - Продавец");
        //while (isValidRole == 0)
        //{
        //    Console.WriteLine("Введите роль:");
        //    role = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Введите имя/название организации: ");
        //    name = Console.ReadLine();
        //    isValidRole = ConsoleRights(role, name);
        //}
        //switch (isValidRole)
        //{
        //    case 1:
        //        ConsoleAdmin();
        //        break;
        //    case 2:
        //        ConsoleCustomer();
        //        break;
        //    case 3:
        //        ConsoleSeller();
        //        break;
        //}
    }
}