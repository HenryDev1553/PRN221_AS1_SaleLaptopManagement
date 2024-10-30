﻿using SaleLaptop_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Repositories.Interface
{
    public interface IOrderDetailRepo
    {
        IEnumerable<OrderDetail> GetOrderDetails();
        IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId);
    }
}
