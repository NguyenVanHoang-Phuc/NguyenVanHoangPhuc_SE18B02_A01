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
    /// Interaction logic for CreateReportWindow.xaml
    /// </summary>
    public partial class CreateReportWindow : Window
    {

        private readonly IBookingReservationRepository _bookingReservationRepository;

        public CreateReportWindow()
        {
            InitializeComponent();
            _bookingReservationRepository = new BookingReservationRepository();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load initial data if needed
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ngày bắt đầu và kết thúc từ DatePicker
            DateOnly startDate = dpStartDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpStartDate.SelectedDate.Value) : DateOnly.MinValue;
            DateOnly endDate = dpEndDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value) : DateOnly.MaxValue;

            // Kiểm tra xem cả hai ngày đã được chọn chưa
            if (startDate == DateOnly.MinValue || endDate == DateOnly.MaxValue)
            {
                MessageBox.Show("Please select both Start Date and End Date.");
                return;
            }

            // Lấy danh sách đặt phòng theo khoảng thời gian đã chọn
            var bookings = _bookingReservationRepository.GetBookingReservationsByDateRange(startDate, endDate)
                .Select(br => new
                {
                    br.BookingReservationId,
                    BookingDate = br.BookingDate?.ToString("dd/MM/yyyy"),
                    TotalPrice = br.TotalPrice.HasValue ? $"{br.TotalPrice:C}" : "",
                    BookingStatus = br.BookingStatus == 1 ? "Confirmed" : "Pending"
                }).ToList();

            // Đặt nguồn dữ liệu cho DataGrid
            dgReportData.ItemsSource = bookings;
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnManageCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate to Manage Customer Information page
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
            // No action needed as we are already in the Room Information Management page
        }
    }
}
