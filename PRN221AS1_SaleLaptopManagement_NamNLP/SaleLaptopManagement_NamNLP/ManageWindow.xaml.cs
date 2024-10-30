using SaleLaptop_BusinessObjects;
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
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        private Account _account;
        public ManageWindow(Account account, string? Logs)
        {
            InitializeComponent();
            MainContent.Children.Add(new CategoryUserControl(account, null, MainContent, null));
            this._account = account;
            labelUser.Content = account.UserName;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                if (MainContent.Children[0] is CategoryUserControl)
                {
                    MainContent.Children.Add(new CategoryUserControl(_account, null, MainContent, searchText));
                }
                else if (MainContent.Children[0] is ProductUserControl)
                {
                    MainContent.Children.Add(new ProductUserControl(_account, null, MainContent, searchText));
                }
            }
        }
        private void Category_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new CategoryUserControl(_account, null, MainContent, null));
            txtSearch.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new ProductUserControl(_account, null, MainContent, null));
            txtSearch.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new OrderUserControl(_account, MainContent));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindown screen = new LoginWindown();
            screen.Show();
            Close();
        }
    }
}
