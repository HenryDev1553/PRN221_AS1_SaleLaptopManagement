using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public interface IOrderService
    {
        public List<Order> GetOrders();
        public Order GetOrder(string id);
        public bool AddOrder(Order order);
      
        public bool UpdateOrder(Order order);
    }
}
