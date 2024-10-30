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
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        private readonly ICategoriesRepo _cateRepo = new CategoriesRepo();
        private string Active;
        private Account _account;
        private CategoryModel? _category;
        private string Logs { get; set; }
        public System.Windows.Controls.Grid MainControl { get; set; }
        public EditCategory(string active, CategoryModel? category, Account account, string? Logs, System.Windows.Controls.Grid mainControl)
        {
            InitializeComponent();
            this.Active = active;
            this._category = category;
            this._account = account;
            this.Logs = Logs;
            this.MainControl = mainControl;
            if (active.Equals("Add") || active.Equals("Update"))
            {
                txtCategoryId.IsReadOnly = true;
            }
            InitializeInput();
        }
        public void InitializeInput()
        {
            if (_category != null)
            {
                txtCategoryId.Text = _category.CategoryId.ToString() ?? string.Empty;
                txtCategoryName.Text = _category.CategoryName ?? string.Empty;
                txtCategoryDesciption.Text = _category.Description ?? string.Empty;
            }
        }
        public Category GetSystemAccountInput(bool isAdd)
        {
            if (isAdd)
            {
                return new Category
                {
                    CategoryName = txtCategoryName.Text ?? string.Empty,
                    Description = txtCategoryDesciption.Text ?? string.Empty,
                };
            }
            return new Category
            {
                CategoryId = int.Parse(txtCategoryId.Text),
                CategoryName = txtCategoryName.Text ?? string.Empty,
                Description = txtCategoryDesciption.Text ?? string.Empty,
            };
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Active.Equals("Add"))
            {
                var category = GetSystemAccountInput(true);
                _cateRepo.AddCategory(category);
                Logs += $"{category.CategoryName} vừa được thêm !\n";
                submit.Content = "Add thành công !";
            }
            else
            {
                var category = GetSystemAccountInput(false);
                _cateRepo.UpdateCategory(category);
                Logs += $"{category.CategoryName} vừa được sửa !\n";
                submit.Content = "Update thành công !";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            CategoryUserControl screen = new CategoryUserControl(_account, Logs, MainControl, null);
            MainControl.Children.Clear();
            MainControl.Children.Add(screen);
            Close();
        }
    }
}
