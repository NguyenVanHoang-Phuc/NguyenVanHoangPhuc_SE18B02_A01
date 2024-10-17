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
    /// Interaction logic for BookingReservationWindow.xaml
    /// </summary>
    public partial class BookingReservationWindow : Window
    {
        private Customer currentUser;
        private readonly IBookingReservationRepository _bookingReservationRepository;


        public BookingReservationWindow(Customer user)
        {
            InitializeComponent();
            currentUser = user;
            _bookingReservationRepository = new BookingReservationRepository();
            LoadBookingHistory();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadBookingHistory()
        {
            if (currentUser == null || currentUser.BookingReservations == null)
            {
                MessageBox.Show("Invalid user data. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var bookingReservations = _bookingReservationRepository.GetAllBookingReservations()
                                  .Where(br => br.CustomerId == currentUser.CustomerId)
                                  .Select(br => new
                                  {
                                      br.BookingReservationId,
                                      br.BookingDate,
                                      br.TotalPrice,
                                      BookingStatus = br.BookingStatus == 1 ? "Confirmed" : "Pending"
                                  }).ToList();

            dgBookingHistory.ItemsSource = bookingReservations;
        }

        private void btnUserProfile_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CustomerProfile customerProfile = new CustomerProfile(currentUser);
            customerProfile.Show();
        }
    }
}
