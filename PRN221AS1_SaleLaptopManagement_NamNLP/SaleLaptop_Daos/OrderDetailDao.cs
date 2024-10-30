using Microsoft.EntityFrameworkCore;
using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Daos
{
    public class OrderDetailDao
    {
        private static OrderDetailDao instance;
        private SaleLaptopManageDBContext dbContext;

        public OrderDetailDao()
        {
            dbContext = new SaleLaptopManageDBContext();
        }
        public static OrderDetailDao Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrderDetailDao();
                return instance;
            }
        }
        public IEnumerable<OrderDetail> GetOrderDetails() => dbContext.OrderDetails.Include(x => x.Product).ToList();
        public OrderDetail GetOrderDetail(string id) => dbContext.OrderDetails.SingleOrDefault(c => c.OrderDetailId.Equals(id));

        public bool AddOrderDetail(OrderDetail orderDetail)
        {

            bool isSuccess = false;
            try
            {
                dbContext.Add(orderDetail);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Update(orderDetail);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            try
            {
                var result = dbContext.OrderDetails.Include(x => x.Product).Where(x => x.OrderId == orderId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
