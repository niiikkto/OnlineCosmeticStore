using DbPain.db;
using DbPain.server.AdminLogic;
using DbPain.server.CustomerLogic;
using DbPain.server.SellerLogic;

namespace DbPain.server
{
    public class BusinessLogicFacade
    {
        private readonly AdminBusinessLogic _adminLogic;
        private readonly SellerBusinessLogic _sellerLogic;
        private readonly CustomerBusinessLogic _customerLogic;

        public BusinessLogicFacade(ShopDbContext context)
        {
            _adminLogic = new AdminBusinessLogic(context);
            _sellerLogic = new SellerBusinessLogic(context);
            _customerLogic = new CustomerBusinessLogic(context);
        }

        #region Admin Operations
        public void ViewAllProducts() => _adminLogic.ViewAllProducts();
        public void ViewAllOrders() => _adminLogic.ViewAllOrders();
        public void ViewAllSellers() => _adminLogic.ViewAllSellers();
        public void ViewAllCustomers() => _adminLogic.ViewAllCustomers();
        public bool DeleteProduct(int productId) => _adminLogic.DeleteProduct(productId);
        public bool DeleteOrder(int orderId) => _adminLogic.DeleteOrder(orderId);
        public bool DeleteSeller(int sellerId) => _adminLogic.DeleteSeller(sellerId);
        public bool DeleteCustomer(int customerId) => _adminLogic.DeleteCustomer(customerId);
        public Admin AuthenticateAdmin(string name, string email) => _adminLogic.AuthenticateAdmin(name, email);
        #endregion

        #region Seller Operations
        public bool AddProduct(string name, string description, double price, int quantity, int sellerId) =>
            _sellerLogic.AddProduct(name, description, price, quantity, sellerId);
        public bool UpdateProduct(int productId, string name, string description, double price, int quantity, int sellerId) =>
            _sellerLogic.UpdateProduct(productId, name, description, price, quantity, sellerId);
        public Seller GetSellerInfo(string name, string email) => _sellerLogic.GetSellerInfo(name, email);
        public Seller AuthenticateSeller(string name, string email) => _sellerLogic.AuthenticateSeller(name, email);
        #endregion

        #region Customer Operations
        public bool AddToBasket(int productId) => _customerLogic.AddToBasket(productId);
        public bool RemoveFromBasket(int basketId) => _customerLogic.RemoveFromBasket(basketId);
        public Customer GetCustomerInfo(string name, string email) => _customerLogic.GetCustomerInfo(name, email);
        public List<Basket> GetCustomerBasket(int customerId) => _customerLogic.GetCustomerBasket(customerId);
        public bool CreateOrder(int customerId, int productId, int paymentMethodId, double price) =>
            _customerLogic.CreateOrder(customerId, productId, paymentMethodId, price);
        public bool CancelOrder(int orderId, int customerId) => _customerLogic.CancelOrder(orderId, customerId);
        public List<Order> GetCustomerOrders(int customerId) => _customerLogic.GetCustomerOrders(customerId);
        public Customer AuthenticateCustomer(string name, string email) => _customerLogic.AuthenticateCustomer(name, email);
        #endregion
    }
} 