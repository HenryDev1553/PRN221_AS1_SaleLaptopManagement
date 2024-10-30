using SaleLaptop_BusinessObjects;
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
    /// Interaction logic for ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        private readonly IProductRepo _productRepo = new ProductRepo();
        private Account _account;
        private System.Windows.Controls.Grid MainControl;

        public ProductUserControl(Account account, string? Logs, System.Windows.Controls.Grid mainControl, string? txtSearch)
        {
            InitializeComponent();
            if (txtSearch != null)
            {
                dgProductList.Columns.Clear();
                var proList = _productRepo.GetProductByName(txtSearch).Select(x => new ProductModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName ?? string.Empty,
                    Brand = x.Brand,
                    Model = x.Model,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryName = x.Category.CategoryName ?? string.Empty
                });
                dgProductList.ItemsSource = proList;
                
            }
            else
            {
                Loaded += RenderDefaultDataGrid;
            }
            this._account = account;
            logs.Content += Logs;
            this.MainControl = mainControl;
        }
        private void RenderDefaultDataGrid(object sender, RoutedEventArgs e)
        {
            dgProductList.Columns.Clear();
            var list = _productRepo.GetProducts().Where(x => x.Category != null).Select(x => new ProductModel
            {
                ProductId = x?.ProductId ?? 0,
                ProductName = x?.ProductName ?? string.Empty,
                Brand = x?.Brand ?? string.Empty,
                Model = x?.Model ?? string.Empty,
                Price = x?.Price ?? 0,
                Stock = x?.Stock ?? 0,
                CategoryName = x?.Category?.CategoryName ?? string.Empty
            });
            dgProductList.ItemsSource = list;
        }

        private void dgProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProductList.SelectedItem != null)
            {
                BtnEdit.Content = "Update";
            }
            else
            {
                BtnEdit.Content = "Add";
            };
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditProduct screen;
            if (BtnEdit.Content == "Update")
            {
                DataGridCellInfo cellInfo = dgProductList.SelectedCells[0];
                object cellValue = cellInfo.Item;
                ProductModel? product = cellValue as ProductModel;
                screen = new EditProduct(BtnEdit.Content.ToString(), _account, logs.Content.ToString(), MainControl, product.ProductId);
            }
            else
            {
                screen = screen = new EditProduct(BtnEdit.Content.ToString(), _account, logs.Content.ToString(), MainControl, 0);
            }
            screen.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductList.SelectedCells.Count > 0)
            {
                DataGridCellInfo cellInfo = dgProductList.SelectedCells[0];
                object cellValue = cellInfo.Item;
                ProductModel? product = cellValue as ProductModel;
                if (product != null)
                {
                    string message = $"Are you sure you want to delete {product?.ProductName}?";
                    string[] actionOptions = { "Delete", "Cancel" };

                    var result = MessageBox.Show(
                        message,
                        "Delete Confirmation",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            var productDel = _productRepo.DeleteProduct(product.ProductId);
                            if (productDel != null)
                            {
                                RenderDefaultDataGrid(null, null);
                                logs.Content += $"{product?.ProductName} vừa bị xóa !\n";
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the account.");
                            }
                            break;
                    }
                }
            }
        }


    }
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public string? CategoryName { get; set; }
    }
}
