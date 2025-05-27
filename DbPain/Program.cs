using DbPain.db_controler;
using DbPain.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Data;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

class Program
{
    public static void ConsoleAdmin(ShopDbContext context, Admin admin)
    {
        bool isExit = false;
        while (isExit == false)
        {
            Console.WriteLine("Выберите действие администратора:");
            Console.WriteLine("1 - Просмотреть все товары");
            Console.WriteLine("2 - Просмотреть все заказы");
            Console.WriteLine("3 - Просмотреть всех продавцов");
            Console.WriteLine("4 - Просмотреть всех покупателей");
            Console.WriteLine("5 - Удалить товар");
            Console.WriteLine("6 - Удалить заказ");
            Console.WriteLine("7 - Удалить продавца");
            Console.WriteLine("8 - Удалить покупателя");
            Console.WriteLine("9 - Выйти");

            int chose;
            if (!int.TryParse(Console.ReadLine(), out chose))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                continue;
            }

            switch (chose)
            {
                case 1:
                    ProductDbControler.Read(context);
                    break;
                case 2:
                    OrderDbControler.Read(context);
                    break;
                case 3:
                    SellerDbControler.Read(context);
                    break;
                case 4:
                    CustomerDbControler.Read(context);
                    break;
                case 5:
                    Console.WriteLine("Введите ID товара для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        ProductDbControler.Delete(context, productId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID товара.");
                    }
                    break;
                case 6:
                    Console.WriteLine("Введите ID заказа для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int orderId))
                    {
                        OrderDbControler.Delete(context, orderId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID заказа.");
                    }
                    break;
                case 7:
                    Console.WriteLine("Введите ID продавца для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int sellerId))
                    {
                        SellerDbControler.Delete(context, sellerId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID продавца.");
                    }
                    break;
                case 8:
                    Console.WriteLine("Введите ID покупателя для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int customerId))
                    {
                        CustomerDbControler.Delete(context, customerId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID покупателя.");
                    }
                    break;
                case 9:
                    isExit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 9.");
                    break;
            }
        }
    }

    public static void ConsoleSeller(ShopDbContext context, Seller seller)
    {
        bool isExit = false;
        while (isExit == false)
        {
            Console.WriteLine("Выберите действие поставщика:");
            Console.WriteLine("1 - Добавить товар на склад");
            Console.WriteLine("2 - Убрать товар со склада");
            Console.WriteLine("3 - Узнать свои данные");
            Console.WriteLine("4 - Поменять данные своего товара");
            Console.WriteLine("5 - Выйти");

            int chose;
            if (!int.TryParse(Console.ReadLine(), out chose))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                continue;
            }

            switch (chose)
            {
                case 1:
                    Console.WriteLine("Введите название товара:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите описание товара:");
                    string description = Console.ReadLine();
                    Console.WriteLine("Введите цену товара:");
                    if (double.TryParse(Console.ReadLine(), out double price))
                    {
                        Console.WriteLine("Введите количество товара:");
                        if (int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            ProductDbControler.Add(context, name, description, price, quantity, seller.Id);
                            Console.WriteLine("Товар успешно добавлен!");
                        }
                        else
                        {
                            Console.WriteLine("Неверное количество.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверная цена.");
                    }
                    break;
                case 2:
                    ProductDbControler.Read(context);
                    Console.WriteLine("Введите ID товара для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        ProductDbControler.Delete(context, productId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID товара.");
                    }
                    break;
                case 3:
                    Console.WriteLine($"Ваши данные:\nИмя: {seller.Name}\nТелефон: {seller.NumberPhone}\nEmail: {seller.Email}");
                    break;
                case 4:
                    ProductDbControler.Read(context);
                    Console.WriteLine("Введите ID товара для изменения:");
                    if (int.TryParse(Console.ReadLine(), out int updateProductId))
                    {
                        Console.WriteLine("Введите новое название товара:");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Введите новое описание товара:");
                        string newDescription = Console.ReadLine();
                        Console.WriteLine("Введите новую цену товара:");
                        if (double.TryParse(Console.ReadLine(), out double newPrice))
                        {
                            Console.WriteLine("Введите новое количество товара:");
                            if (int.TryParse(Console.ReadLine(), out int newQuantity))
                            {
                                ProductDbControler.Update(context, updateProductId, newName, newDescription, newPrice, newQuantity, seller.Id);
                                Console.WriteLine("Товар успешно обновлен!");
                            }
                            else
                            {
                                Console.WriteLine("Неверное количество.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверная цена.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID товара.");
                    }
                    break;
                case 5:
                    isExit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 5.");
                    break;
            }
        }
    }

    public static void ConsoleCustomer(ShopDbContext context, Customer customer)
    {
        bool isExit = false;
        while (isExit == false)
        {
            Console.WriteLine("Выберите действие пользователя:");
            Console.WriteLine("1 - Добавить товар в корзину");
            Console.WriteLine("2 - Убрать товар из корзины");
            Console.WriteLine("3 - Узнать свои данные");
            Console.WriteLine("4 - Оформить заказ");
            Console.WriteLine("5 - Отменить заказ");
            Console.WriteLine("6 - Выйти");

            int chose;
            if (!int.TryParse(Console.ReadLine(), out chose))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                continue;
            }

            switch (chose)
            {
                case 1:
                    ProductDbControler.Read(context);
                    Console.WriteLine("Введите ID продукта:");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        BasketDbControler.Add(context, productId);
                        Console.WriteLine("Товар добавлен в корзину!");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID продукта.");
                    }
                    break;
                case 2:
                    var customerBaskets = context.Baskets
                        .Where(b => b.Id == customer.BasketId)
                        .Include(b => b.Product)
                        .ToList();

                    if (customerBaskets.Any())
                    {
                        Console.WriteLine("Ваша корзина:");
                        foreach (var basket in customerBaskets)
                        {
                            Console.WriteLine($"ID: {basket.Id}, Товар: {basket.Product.Name}");
                        }

                        Console.WriteLine("Введите ID товара для удаления из корзины:");
                        if (int.TryParse(Console.ReadLine(), out int basketId))
                        {
                            BasketDbControler.Delete(context, basketId);
                            Console.WriteLine("Товар удален из корзины!");
                        }
                        else
                        {
                            Console.WriteLine("Неверный ID товара.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ваша корзина пуста.");
                    }
                    break;
                case 3:
                    Console.WriteLine($"Ваши данные:\nИмя: {customer.Name}\nТелефон: {customer.NumberPhone}\nАдрес доставки: {customer.DeliveryAddress}\nEmail: {customer.Email}");
                    break;
                case 4:
                    var baskets = context.Baskets
                        .Where(b => b.Id == customer.BasketId)
                        .Include(b => b.Product)
                        .ToList();

                    if (baskets.Any())
                    {
                        PaymentMethodDbControler.Read(context);
                        Console.WriteLine("Выберите способ оплаты (введите ID):");
                        if (int.TryParse(Console.ReadLine(), out int paymentMethodId))
                        {
                            double totalPrice = baskets.Sum(b => b.Product.Price);
                            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                            foreach (var basket in baskets)
                            {
                                OrderDbControler.Add(
                                    context,
                                    customer.Id,
                                    currentDate,
                                    1, // quantity
                                    basket.ProductId,
                                    paymentMethodId,
                                    Status.Issued.ToString(),
                                    basket.Product.Price
                                );

                                // Remove from basket after ordering
                                BasketDbControler.Delete(context, basket.Id);
                            }

                            Console.WriteLine($"Заказ оформлен! Общая сумма: {totalPrice}");
                        }
                        else
                        {
                            Console.WriteLine("Неверный ID способа оплаты.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ваша корзина пуста. Невозможно оформить заказ.");
                    }
                    break;
                case 5:
                    OrderDbControler.Read(context);
                    Console.WriteLine("Введите ID заказа для отмены:");
                    if (int.TryParse(Console.ReadLine(), out int orderId))
                    {
                        var order = context.Orders.Find(orderId);
                        if (order != null && order.CustomerId == customer.Id)
                        {
                            OrderDbControler.Delete(context, orderId);
                            Console.WriteLine("Заказ отменен!");
                        }
                        else
                        {
                            Console.WriteLine("Заказ не найден или не принадлежит вам.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID заказа.");
                    }
                    break;
                case 6:
                    isExit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 6.");
                    break;
            }
        }
    }

    public static void Role(ShopDbContext context)
    {
        bool isNotExit = true;
        while (isNotExit)
        {
            Console.WriteLine("Выберите роль:");
            Console.WriteLine("1 - Админ");
            Console.WriteLine("2 - Пользователь");
            Console.WriteLine("3 - Продавец");
            Console.WriteLine("4 - Выйти");

            int role;
            if (!int.TryParse(Console.ReadLine(), out role))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                continue;
            }

            string name;
            string email;
            switch (role)
            {
                case 1:
                    Console.WriteLine("Введите имя");
                    name = Console.ReadLine();
                    Console.WriteLine("Введите почту");
                    email = Console.ReadLine();
                    Admin admin = context.Admins.FirstOrDefault(p => p.Name == name && p.Email == email);
                    if (admin != null)
                    {
                        ConsoleAdmin(context, admin);
                    }
                    else
                    {
                        Console.WriteLine("Админ не найден");
                    }
                    break;
                case 2:
                    Console.WriteLine("Введите имя");
                    name = Console.ReadLine();
                    Console.WriteLine("Введите почту");
                    email = Console.ReadLine();
                    var customer = context.Customers.FirstOrDefault(p => p.Name == name && p.Email == email);
                    if (customer != null)
                    {
                        ConsoleCustomer(context, customer);
                    }
                    else
                    {
                        Console.WriteLine("Пользователь не найден");
                    }
                    break;
                case 3:
                    Console.WriteLine("Введите имя");
                    name = Console.ReadLine();
                    Console.WriteLine("Введите почту");
                    email = Console.ReadLine();
                    var seller = context.Sellers.FirstOrDefault(p => p.Name == name && p.Email == email);
                    if (seller != null)
                    {
                        ConsoleSeller(context, seller);
                    }
                    else
                    {
                        Console.WriteLine("Продавец не найден");
                    }
                    break;
                case 4:
                    isNotExit = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 4.");
                    break;
            }
        }
    }

    public static void Main()
    {
        var context = new ShopDbContext();
        context.Database.EnsureCreated();
        //GenerateTestData(context);
        CustomerDbControler.Read(context);
        Role(context);
    }
}