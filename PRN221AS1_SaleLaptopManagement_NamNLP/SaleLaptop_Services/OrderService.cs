using SaleLaptop_BusinessObjects;
using SaleLaptop_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleLaptop_Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepo orderRepo;
        public OrderService() {
            orderRepo = new OrderRepo();
        }

        public bool AddOrder(Order order)=> orderRepo.AddOrder(order);
        

        public Order GetOrder(string id)=> orderRepo.GetOrder(id);


        public List<Order> GetOrders() => orderRepo.GetOrders();
        public bool UpdateOrder(Order order)=> orderRepo.UpdateOrder(order);
        
    }
}
