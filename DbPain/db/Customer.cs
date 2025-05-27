using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbPain.db
{
    internal class Customer
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
}
