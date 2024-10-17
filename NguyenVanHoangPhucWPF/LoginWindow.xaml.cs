using BusinessOjects;
using Repositories;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private readonly ICustomerRepository _customerRepository;

        public LoginWindow()
        {
            InitializeComponent();
            _customerRepository = new CustomerRepository();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text;
            string password = txtPass.Password;

            // Thông tin tài khoản quản trị viên
            const string adminEmail = "admin@FUMiniHotelSystem.com";
            const string adminPassword = "@@abc123@@";

            // Kiểm tra tài khoản quản trị viên
            if (email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && password == adminPassword)
            {
                // Hiển thị MainWindow cho quản trị viên
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                return;
            }

            // Kiểm tra tài khoản từ cơ sở dữ liệu
            Customer? account = _customerRepository.GetCustomerByEmail(email);

            // Kiểm tra tài khoản và mật khẩu
            if (account != null && account.Password == password) // So sánh mật khẩu
            {
                this.Hide();
                CustomerProfile customerProfile = new CustomerProfile(account);
                customerProfile.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
