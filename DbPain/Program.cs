using DbPain.db;
using DbPain.db_controler;
using DbPain.server.AdminLogic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using DbPain.server;

class Program
{
    private static BusinessLogicFacade _businessLogic;

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
                    _businessLogic.ViewAllProducts();
                    break;
                case 2:
                    _businessLogic.ViewAllOrders();
                    break;
                case 3:
                    _businessLogic.ViewAllSellers();
                    break;
                case 4:
                    _businessLogic.ViewAllCustomers();
                    break;
                case 5:
                    Console.WriteLine("Введите ID товара для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        if (_businessLogic.DeleteProduct(productId))
                            Console.WriteLine("Товар успешно удален!");
                        else
                            Console.WriteLine("Ошибка при удалении товара.");
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
                        if (_businessLogic.DeleteOrder(orderId))
                            Console.WriteLine("Заказ успешно удален!");
                        else
                            Console.WriteLine("Ошибка при удалении заказа.");
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
                        if (_businessLogic.DeleteSeller(sellerId))
                            Console.WriteLine("Продавец успешно удален!");
                        else
                            Console.WriteLine("Ошибка при удалении продавца.");
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
                        if (_businessLogic.DeleteCustomer(customerId))
                            Console.WriteLine("Покупатель успешно удален!");
                        else
                            Console.WriteLine("Ошибка при удалении покупателя.");
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
                            if (_businessLogic.AddProduct(name, description, price, quantity, seller.Id))
                                Console.WriteLine("Товар успешно добавлен!");
                            else
                                Console.WriteLine("Ошибка при добавлении товара.");
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
                    _businessLogic.ViewAllProducts();
                    Console.WriteLine("Введите ID товара для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        if (_businessLogic.DeleteProduct(productId))
                            Console.WriteLine("Товар успешно удален!");
                        else
                            Console.WriteLine("Ошибка при удалении товара.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID товара.");
                    }
                    break;
                case 3:
                    var sellerInfo = _businessLogic.GetSellerInfo(seller.Name, seller.Email);
                    if (sellerInfo != null)
                    {
                        Console.WriteLine($"Ваши данные:\nИмя: {sellerInfo.Name}\nТелефон: {sellerInfo.NumberPhone}\nEmail: {sellerInfo.Email}");
                    }
                    break;
                case 4:
                    _businessLogic.ViewAllProducts();
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
                                if (_businessLogic.UpdateProduct(updateProductId, newName, newDescription, newPrice, newQuantity, seller.Id))
                                    Console.WriteLine("Товар успешно обновлен!");
                                else
                                    Console.WriteLine("Ошибка при обновлении товара.");
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
                    _businessLogic.ViewAllProducts();
                    Console.WriteLine("Введите ID продукта:");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        if (_businessLogic.AddToBasket(productId))
                            Console.WriteLine("Товар добавлен в корзину!");
                        else
                            Console.WriteLine("Ошибка при добавлении товара в корзину.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ID продукта.");
                    }
                    break;
                case 2:
                    var customerBaskets = _businessLogic.GetCustomerBasket(customer.Id);
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
                            if (_businessLogic.RemoveFromBasket(basketId))
                                Console.WriteLine("Товар удален из корзины!");
                            else
                                Console.WriteLine("Ошибка при удалении товара из корзины.");
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
                    var customerInfo = _businessLogic.GetCustomerInfo(customer.Name, customer.Email);
                    if (customerInfo != null)
                    {
                        Console.WriteLine($"Ваши данные:\nИмя: {customerInfo.Name}\nТелефон: {customerInfo.NumberPhone}\nАдрес доставки: {customerInfo.DeliveryAddress}\nEmail: {customerInfo.Email}");
                    }
                    break;
                case 4:
                    var baskets = _businessLogic.GetCustomerBasket(customer.Id);
                    if (baskets.Any())
                    {
                        PaymentMethodDbControler.Read(context);
                        Console.WriteLine("Выберите способ оплаты (введите ID):");
                        if (int.TryParse(Console.ReadLine(), out int paymentMethodId))
                        {
                            double totalPrice = baskets.Sum(b => b.Product.Price);
                            bool allOrdersCreated = true;

                            foreach (var basket in baskets)
                            {
                                if (!_businessLogic.CreateOrder(customer.Id, basket.ProductId, paymentMethodId, basket.Product.Price))
                                {
                                    allOrdersCreated = false;
                                    break;
                                }
                                _businessLogic.RemoveFromBasket(basket.Id);
                            }

                            if (allOrdersCreated)
                                Console.WriteLine($"Заказ оформлен! Общая сумма: {totalPrice}");
                            else
                                Console.WriteLine("Ошибка при оформлении заказа.");
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
                    var orders = _businessLogic.GetCustomerOrders(customer.Id);
                    if (orders.Any())
                    {
                        Console.WriteLine("Ваши заказы:");
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"ID: {order.Id}, Товар: {order.Product.Name}, Статус: {order.Status}");
                        }

                        Console.WriteLine("Введите ID заказа для отмены:");
                        if (int.TryParse(Console.ReadLine(), out int orderId))
                        {
                            if (_businessLogic.CancelOrder(orderId, customer.Id))
                                Console.WriteLine("Заказ отменен!");
                            else
                                Console.WriteLine("Ошибка при отмене заказа.");
                        }
                        else
                        {
                            Console.WriteLine("Неверный ID заказа.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("У вас нет активных заказов.");
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
            CustomerDbControler.Read(context);

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
                    var admin = _businessLogic.AuthenticateAdmin(name, email);
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
                    var customer = _businessLogic.AuthenticateCustomer(name, email);
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
                    var seller = _businessLogic.AuthenticateSeller(name, email);
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
        _businessLogic = new BusinessLogicFacade(context);
        //GenerateTestData(context);
        Role(context);
    }
}