using SaleLaptop_BusinessObjects;
using SaleLaptop_Repositories;
using SaleLaptop_Repositories.Interface;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindown : Window
    {
        private IAccountRepo _accountRepo = null;
        public LoginWindown()
        {
            InitializeComponent();
            if (_accountRepo == null)
            {
                _accountRepo = new AccountRepo();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Please enter email and password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Account account = _accountRepo.Login(txtEmail.Text, txtPassword.Password);

            if (account == null)
            {
                MessageBox.Show("Login failed! Please check your email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (account != null && account.RoleId == 1)
            {
                ManageWindow manageWindown = new ManageWindow(account,null);
                manageWindown.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnQuit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Do you want to exit the app?", "Exit App!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}