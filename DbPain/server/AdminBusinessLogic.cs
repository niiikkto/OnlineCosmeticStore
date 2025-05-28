using DbPain.db;
using DbPain.db_controler;
using DbPain.server.Base;

namespace DbPain.server.AdminLogic
{
    public class AdminBusinessLogic : BaseBusinessLogic
    {
        public AdminBusinessLogic(ShopDbContext context) : base(context)
        {
        }

        public void ViewAllProducts()
        {
            ProductDbControler.Read(_context);
        }

        public void ViewAllOrders()
        {
            OrderDbControler.Read(_context);
        }

        public void ViewAllSellers()
        {
            SellerDbControler.Read(_context);
        }

        public void ViewAllCustomers()
        {
            CustomerDbControler.Read(_context);
        }

        public bool DeleteProduct(int productId)
        {
            return ExecuteOperation(() => ProductDbControler.Delete(_context, productId));
        }

        public bool DeleteOrder(int orderId)
        {
            return ExecuteOperation(() => OrderDbControler.Delete(_context, orderId));
        }

        public bool DeleteSeller(int sellerId)
        {
            return ExecuteOperation(() => SellerDbControler.Delete(_context, sellerId));
        }

        public bool DeleteCustomer(int customerId)
        {
            return ExecuteOperation(() => CustomerDbControler.Delete(_context, customerId));
        }

        public Admin AuthenticateAdmin(string name, string email)
        {
            return _context.Admins.FirstOrDefault(p => p.Name == name && p.Email == email);
        }
    }
} 