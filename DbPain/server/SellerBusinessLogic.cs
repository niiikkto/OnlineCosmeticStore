using DbPain.db;
using DbPain.db_controler;
using DbPain.server.Base;

namespace DbPain.server.SellerLogic
{
    public class SellerBusinessLogic : BaseBusinessLogic
    {
        public SellerBusinessLogic(ShopDbContext context) : base(context)
        {
        }

        public bool AddProduct(string name, string description, double price, int quantity, int sellerId)
        {
            return ExecuteOperation(() => 
                ProductDbControler.Add(_context, name, description, price, quantity, sellerId));
        }

        public bool UpdateProduct(int productId, string name, string description, double price, int quantity, int sellerId)
        {
            return ExecuteOperation(() => 
                ProductDbControler.Update(_context, productId, name, description, price, quantity, sellerId));
        }

        public Seller GetSellerInfo(string name, string email)
        {
            return _context.Sellers.FirstOrDefault(p => p.Name == name && p.Email == email);
        }

        public Seller AuthenticateSeller(string name, string email)
        {
            return _context.Sellers.FirstOrDefault(p => p.Name == name && p.Email == email);
        }
    }
} 