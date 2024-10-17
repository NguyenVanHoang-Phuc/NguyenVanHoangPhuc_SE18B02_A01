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
    /// Interaction logic for CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfile : Window
    {
        private Customer currentUser;
        private readonly ICustomerRepository _customerRepository;

        public CustomerProfile(Customer user)
        {
            InitializeComponent();
            currentUser = user;
            _customerRepository = new CustomerRepository();
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            txtCustomerID.Text = currentUser.CustomerId.ToString();
            txtCustomerFullName.Text = currentUser.CustomerFullName;
            txtTelephone.Text = currentUser.Telephone;
            txtEmailAddress.Text = currentUser.EmailAddress;
            dpCustomerBirthday.SelectedDate = currentUser.CustomerBirthday.Value.ToDateTime(TimeOnly.MinValue);
            cboCustomerStatus.SelectedIndex = currentUser.CustomerStatus == 1 ? 0 : 1;
            pwdPassword.Password = currentUser.Password;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            currentUser.CustomerFullName = txtCustomerFullName.Text;
            currentUser.Telephone = txtTelephone.Text;
            currentUser.EmailAddress = txtEmailAddress.Text;
            currentUser.CustomerBirthday = DateOnly.FromDateTime(dpCustomerBirthday.SelectedDate.Value);
            currentUser.CustomerStatus = cboCustomerStatus.SelectedIndex == 0 ? (byte)1 : (byte)0;
            currentUser.Password = pwdPassword.Password;
            _customerRepository.UpdateCustomer(currentUser);
            LoadUserProfile();
            MessageBox.Show("Profile updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingReservationWindow bookingReservationWindow = new BookingReservationWindow(currentUser);
            bookingReservationWindow.Show();
        }
    }
}
