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
    public class OrderRepo : IOrderRepo
    {
        

        public IEnumerable<Order> GetOrders()
        {
            try
            {
                var result = OrderDao.Instance.GetOrders();
                if (result == null) throw new Exception("Order is Empty");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
