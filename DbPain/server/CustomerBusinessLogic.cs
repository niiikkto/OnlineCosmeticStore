using DbPain.db;
using DbPain.db_controler;
using DbPain.server.Base;
using Microsoft.EntityFrameworkCore;

namespace DbPain.server.CustomerLogic
{
    public class CustomerBusinessLogic : BaseBusinessLogic
    {
        public CustomerBusinessLogic(ShopDbContext context) : base(context)
        {
        }

        public bool AddToBasket(int productId)
        {
            return ExecuteOperation(() => BasketDbControler.Add(_context, productId));
        }

        public bool RemoveFromBasket(int basketId)
        {
            return ExecuteOperation(() => BasketDbControler.Delete(_context, basketId));
        }

        public Customer GetCustomerInfo(string name, string email)
        {
            return _context.Customers.FirstOrDefault(p => p.Name == name && p.Email == email);
        }

        public List<Basket> GetCustomerBasket(int customerId)
        {
            return _context.Baskets
                .Where(b => b.Id == customerId)
                .Include(b => b.Product)
                .ToList();
        }

        public bool CreateOrder(int customerId, int productId, int paymentMethodId, double price)
        {
            return ExecuteOperation(() =>
            {
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                OrderDbControler.Add(
                    _context,
                    customerId,
                    currentDate,
                    1,
                    productId,
                    paymentMethodId,
                    Status.Issued.ToString(),
                    price
                );
            });
        }

        public bool CancelOrder(int orderId, int customerId)
        {
            return ExecuteOperation(() =>
            {
                var order = _context.Orders.Find(orderId);
                if (order != null && order.CustomerId == customerId)
                {
                    OrderDbControler.Delete(_context, orderId);
                }
            });
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Product)
                .Include(o => o.PaymentMethod)
                .ToList();
        }

        public Customer AuthenticateCustomer(string name, string email)
        {
            return _context.Customers.FirstOrDefault(p => p.Name == name && p.Email == email);
        }
    }
} 