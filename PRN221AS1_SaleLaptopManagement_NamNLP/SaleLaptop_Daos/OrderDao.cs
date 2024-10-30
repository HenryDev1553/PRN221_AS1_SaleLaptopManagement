using Microsoft.EntityFrameworkCore;
using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Daos
{
    public class OrderDao
    {
        private static OrderDao instance = null;
        private SaleLaptopManageDBContext dbContext;

        public OrderDao()
        {
            dbContext = new SaleLaptopManageDBContext();
        }
        public static OrderDao Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrderDao();
                return instance;
            }
        }
        public List<Order> GetOrders() => dbContext.Orders.ToList();
        public Order GetOrder(string id) => dbContext.Orders.SingleOrDefault(c => c.OrderId.Equals(id));

        public bool AddOrder(Order order)
        {

            bool isSuccess = false;
            try
            {
                dbContext.Add(order);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool UpdateOrder(Order order)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Update(order);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
       
    }
}
