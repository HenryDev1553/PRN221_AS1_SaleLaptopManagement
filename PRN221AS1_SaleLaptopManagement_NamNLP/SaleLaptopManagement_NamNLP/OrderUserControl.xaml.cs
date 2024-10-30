using SaleLaptop_BusinessObjects;
using SaleLaptop_Daos;
using SaleLaptop_Repositories;
using SaleLaptop_Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaleLaptopManagement_NamNLP
{
    /// <summary>
    /// Interaction logic for OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl
    {
        private readonly IOrderRepo _orderRepo = new OrderRepo();
        private readonly IOrderDetailRepo _orderDetailRepo = new OrderDetailRepo();
        private Account _account;
        private System.Windows.Controls.Grid MainControl;
        public OrderUserControl(Account account, System.Windows.Controls.Grid mainControl)
        {
            InitializeComponent();
            var orderList = _orderRepo.GetOrders().Select(x => new
            {
                x.OrderId,
                x.OrderDate,
                x.TotalAmount
            }).ToList();
            _account = account;
            MainControl = mainControl;
            LoadOrders();
        }
        private void LoadOrders()
        {
            var orders = _orderRepo.GetOrders();
            dgOrder.ItemsSource = orders;
        }
        private void LoadOrderDetails(int orderId)
        {
            var orderDetails = GetOrderDetailByOrderId(orderId);
            dgOrderDetail.ItemsSource = orderDetails;
        }

        private void OrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrder.SelectedItem is Order selectedOrder)
            {
                // Ensure OrderDetails include Product information
                dgOrder.ItemsSource = selectedOrder.OrderDetails.Select(od => new
                {
                    od.OrderId,
                    od.Product.ProductName,
                    od.Quantity,
                    od.UnitPrice
                }).ToList();
            }
        }
        private IEnumerable<OrderDetailModel> GetOrderDetailByOrderId(int orderId)
        {
            var list = _orderDetailRepo.GetOrderDetailByOrderId(orderId).Select(x => new OrderDetailModel
            {
                OrderDetailId = x.OrderDetailId,
                OrderId = x.OrderId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                ProductName = x.Product?.ProductName ?? "Unknown Product"
            }).ToList();
            return list;
        }
        private void dgOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrder.SelectedItem is Order selectedOrder)
            {
                // Load and display the order details for the selected order
                LoadOrderDetails(selectedOrder.OrderId);
            }
        }
    }
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
    }
}
