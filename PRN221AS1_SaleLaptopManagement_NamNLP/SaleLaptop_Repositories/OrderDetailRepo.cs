using SaleLaptop_BusinessObjects;
using SaleLaptop_Daos;
using SaleLaptop_Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Repositories
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            try
            {
                var ordd = OrderDetailDao.Instance.GetOrderDetailByOrderId(orderId);
                if (ordd == null) throw new Exception("Order Detail is Empty");
                return ordd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            try
            {
                var ordd = OrderDetailDao.Instance.GetOrderDetails();
                if (ordd == null) throw new Exception("OrderDetail is Empty");
                return ordd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
