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
    /// Interaction logic for CategoryUserControl.xaml
    /// </summary>
    public partial class CategoryUserControl : UserControl
    {
        private readonly ICategoriesRepo categoryRepository = new CategoriesRepo();

        private Account _account;

        private System.Windows.Controls.Grid MainControl;
        public CategoryUserControl(Account account, string? Logs, System.Windows.Controls.Grid mainControl, string? txtSearch)
        {

            InitializeComponent();
            if (txtSearch != null)
            {
                dgCategoryList.Columns.Clear();
                dgCategoryList.ItemsSource = categoryRepository.GetCategoriesByName(txtSearch);
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
            dgCategoryList.Columns.Clear();
            var list = categoryRepository.GetCategories().Select(x => new CategoryModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Description = x.Description
            });
            dgCategoryList.ItemsSource = list;
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditCategory screen;
            if (BtnEdit.Content == "Update")
            {
                DataGridCellInfo cellInfo = dgCategoryList.SelectedCells[0];
                object cellValue = cellInfo.Item;
                CategoryModel? category = cellValue as CategoryModel;
                screen = new EditCategory(BtnEdit.Content.ToString(), category, _account, logs.Content.ToString(), MainControl);
            }
            else
            {
                screen = new EditCategory(BtnEdit.Content.ToString(), null, _account, logs.Content.ToString(), MainControl);
            }
            screen.Show();
        }

        private void dgCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategoryList.SelectedItem != null)
            {
                BtnEdit.Content = "Update";
            }
            else
            {
                BtnEdit.Content = "Add";
            };
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCategoryList.SelectedCells.Count > 0)
            {
                DataGridCellInfo cellInfo = dgCategoryList.SelectedCells[0];
                object cellValue = cellInfo.Item;
                CategoryModel? category = cellValue as CategoryModel;
                if (category != null)
                {
                    string message = $"Are you sure you want to delete category {category?.CategoryName}?";
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
                            try
                            {
                                var categoryDeleted = categoryRepository.DeleteCategory(category.CategoryId);
                                if (categoryDeleted != null)
                                {
                                    RenderDefaultDataGrid(null, null);
                                    logs.Content += $"{category.CategoryName} has been deleted!\n";
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete the category.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "Deletion Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            break;
                    }
                }
            }
            //if (dgCategoryList.SelectedCells.Count > 0)
            //{
            //    DataGridCellInfo cellInfo = dgCategoryList.SelectedCells[0];
            //    object cellValue = cellInfo.Item;
            //    CategoryModel? category = cellValue as CategoryModel;
            //    if (category != null)
            //    {
            //        string message = $"Are you sure you want to delete account {category?.CategoryName}?";
            //        string[] actionOptions = { "Delete", "Cancel" };

            //        var result = MessageBox.Show(
            //            message,
            //            "Delete Confirmation",
            //            MessageBoxButton.YesNo,
            //            MessageBoxImage.Question
            //        );
            //        switch (result)
            //        {
            //            case MessageBoxResult.Yes:
            //                var categoryDeleted = categoryRepository.DeleteCategory(category.CategoryId);
            //                if (categoryDeleted != null)
            //                {
            //                    RenderDefaultDataGrid(null, null);
            //                    logs.Content += $"{category.CategoryName} vừa bị xóa !\n";
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Failed to delete the account.");
            //                }
            //                break;
            //        }
            //    }
            //}
        }
    }
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
