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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICustomerRepository _customerRepository;

        public MainWindow()
        {
            InitializeComponent();
            _customerRepository = new CustomerRepository();
        }

        public void LoadCustomerList()
        {
            try
            {
                var customerList = _customerRepository.GetAllCustomers();
                dgData.ItemsSource = customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of customers");
            }
            finally
            {
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomerList();
        }

    private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = new Customer
                {
                    CustomerFullName = txtCustomerFullName.Text,
                    Telephone = txtTelePhone.Text,
                    EmailAddress = txtEmailAddress.Text,
                    CustomerBirthday = dpCustomerBirthday.SelectedDate != null ? DateOnly.FromDateTime(dpCustomerBirthday.SelectedDate.Value) : null,
                    CustomerStatus = byte.TryParse((cboCustomerStatus.SelectedItem as ComboBoxItem)?.Tag?.ToString(), out byte status) ? status : (byte)0,
                    Password = pwdPassword.Password
                };
                _customerRepository.CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadCustomerList();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem == null)
                return;

            Customer? selectedCustomer = dgData.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                txtCustomerID.Text = selectedCustomer.CustomerId.ToString();
                txtCustomerFullName.Text = selectedCustomer.CustomerFullName;
                txtTelePhone.Text = selectedCustomer.Telephone;
                txtEmailAddress.Text = selectedCustomer.EmailAddress;
                dpCustomerBirthday.SelectedDate = selectedCustomer.CustomerBirthday?.ToDateTime(TimeOnly.MinValue);
                cboCustomerStatus.SelectedIndex = selectedCustomer.CustomerStatus == 1 ? 0 : 1;
                pwdPassword.Password = selectedCustomer.Password;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCustomerID.Text.Length > 0)
                {
                    Customer customer = new Customer
                    {
                        CustomerId = Int32.Parse(txtCustomerID.Text),
                        CustomerFullName = txtCustomerFullName.Text,
                        Telephone = txtTelePhone.Text,
                        EmailAddress = txtEmailAddress.Text,
                        CustomerBirthday = dpCustomerBirthday.SelectedDate != null ? DateOnly.FromDateTime(dpCustomerBirthday.SelectedDate.Value) : null,
                        CustomerStatus = byte.TryParse((cboCustomerStatus.SelectedItem as ComboBoxItem)?.Tag?.ToString(), out byte status) ? status : (byte)0,
                        Password = pwdPassword.Password
                    };
                    _customerRepository.UpdateCustomer(customer);
                }
                else
                {
                    MessageBox.Show("You must select a Customer!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCustomerList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCustomerID.Text.Length > 0)
                {
                    Customer customer = new Customer
                    {
                        CustomerId = Int32.Parse(txtCustomerID.Text)
                    };
                    _customerRepository.DeleteCustomer(customer);
                }
                else
                {
                    MessageBox.Show("You must select a Customer!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCustomerList();
            }
        }

        private void resetInput()
        {
            txtCustomerID.Text = "";
            txtCustomerFullName.Text = "";
            txtTelePhone.Text = "";
            txtEmailAddress.Text = "";
            dpCustomerBirthday.SelectedDate = null;
            cboCustomerStatus.SelectedIndex = 0;
            pwdPassword.Password = "";
        }


        private void btnManageCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate to Manage Customer Information page
            // No action needed as we are already in the Room Information Management page
        }

        private void btnManageRoomInfo_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate to Manage Room Information page
            this.Hide();
            RoomInformationManagement manageRoomInfoWindow = new RoomInformationManagement();
            manageRoomInfoWindow.Show();
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate to Create Report page
            this.Hide();
            CreateReportWindow createReportWindow = new CreateReportWindow();
            createReportWindow.Show();
        }



    }
}