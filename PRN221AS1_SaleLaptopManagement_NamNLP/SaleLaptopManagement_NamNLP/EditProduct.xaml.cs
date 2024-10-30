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
using System.Windows.Shapes;

namespace SaleLaptopManagement_NamNLP
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        private ICategoriesRepo _cateRepo = new CategoriesRepo();
        private IProductRepo _productRepo = new ProductRepo();

        public Dictionary<string, Category> CateAllMap = new Dictionary<string, Category>();
        private string Active;
        private Account _account;
        private string Logs { get; set; }
        private Product? products;
        private List<CategoryItem> SelectCate;
        private System.Windows.Controls.Grid MainControl { get; set; }

        public EditProduct(string active, Account account, string? logs, System.Windows.Controls.Grid mainControl, int productId)
        {
            InitializeComponent();
            this.Active = active;
            this._account = account;
            this.Logs = logs;
            this.MainControl = mainControl;
            if (active.Equals("Update"))
            {
                this.products = _productRepo.GetProductById(productId);
            }
            if (active.Equals("Add") || active.Equals("Update"))
            {
                txtProductId.IsReadOnly = true;
            }
            RenderCateMap();
            RenderBoxList();
            InitializeInput();
        }

        public void InitializeInput()
        {
            if (products != null && Active.Equals("Update"))
            {
                txtProductId.Text = products?.ProductId.ToString() ?? string.Empty;
                txtProductName.Text = products?.ProductName ?? string.Empty;
                txtBrand.Text = products?.Brand ?? string.Empty;
                txtModel.Text = products?.Model ?? string.Empty;
                txtPrice.Text = products?.Price.ToString() ?? string.Empty;
                txtStock.Text = products?.Stock.ToString() ?? string.Empty;
                cmbSelectCate.SelectedIndex = 0;

            }
            else if (products == null || Active.Equals("Add"))
            {
                var newProductId = _productRepo.GetProducts().Select(x => x.ProductId).DefaultIfEmpty(1).Max();
                txtProductId.Text = (newProductId +1).ToString();
                var firstCate = CateAllMap[CateAllMap.Keys.First()].CategoryName;
                cmbSelectCate.SelectedItem = SelectCate.FirstOrDefault(cateItem => cateItem.CategoryName == firstCate);
            }
            submit.Content = $"Xin chào {_account.UserName}";
        }
        private void RenderCateMap()
        {
            var cates = _cateRepo.GetCategories();
            foreach (var cate in cates)
            {
                CateAllMap.Add(cate.CategoryName, cate);
            }
        }
        private void RenderBoxList()
        {
            SelectCate = new List<CategoryItem>();
            foreach (var cateItem in CateAllMap.Keys)
            {
                SelectCate.Add(new CategoryItem { CategoryName = cateItem });
            };
            cmbSelectCate.ItemsSource = SelectCate;
        }
        private void cmbSelectCate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSelectCate.SelectedItem != null)
            {
                // Get the selected item
                CategoryItem selectedCate = cmbSelectCate.SelectedItem as CategoryItem;
            }
        }
        private Category? getCurrentCate()
        {
            if (cmbSelectCate.SelectedItem != null)
            {
                // Get the selected item
                CategoryItem selectedTag = cmbSelectCate.SelectedItem as CategoryItem;

                // Do something with the selected item
                return CateAllMap[selectedTag.CategoryName];
            }
            return null;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                ProductId = int.Parse(txtProductId.Text),
                ProductName = txtProductName.Text,
                Brand = txtBrand.Text,
                Model = txtModel.Text,
                Price = decimal.Parse(txtPrice.Text),
                CategoryId = getCurrentCate().CategoryId,
                Stock = int.Parse(txtStock.Text),
            };
            if (Active.Equals("Add"))
            {
                _productRepo.AddProduct(product);
                Logs += $"Product {product.ProductName} vừa được thêm !\n";
                submit.Content = "Add thành công";
            }
            else if (Active.Equals("Update"))
            {
                _productRepo.UpdateProduct(product);
                Logs += $"Product {product.ProductName} vừa được sửa !\n";
                submit.Content = "Update thành công";
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ProductUserControl screen = new ProductUserControl(_account, Logs, MainControl, null);
            MainControl.Children.Clear();
            MainControl.Children.Add(screen);
            Close();
        }
    }
    public class CategoryItem
    {
        public string CategoryName { get; set; }
        public bool IsSelected { get; set; }
    }
}
