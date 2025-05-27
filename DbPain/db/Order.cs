using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbPain.db
{
    internal class Order
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
}
