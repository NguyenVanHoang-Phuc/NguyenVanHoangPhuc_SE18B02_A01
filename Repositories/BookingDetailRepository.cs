using BusinessOjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public List<BookingDetail> GetBookingDetails() => BookingDetailDAO.Instance.GetBookingDetails();

        public void CreateBookingDetail(BookingDetail b) => BookingDetailDAO.Instance.CreateBookingDetail(b);


        public void UpdateBookingDetail(BookingDetail b) => BookingDetailDAO.Instance.UpdateBookingDetail(b);


        public void DeleteBookingDetail(BookingDetail b) => BookingDetailDAO.Instance.DeleteBookingDetail(b);


        public BookingDetail? GetBookingDetailById(int bookingReservationId, int roomId) => BookingDetailDAO.Instance.GetBookingDetailById(bookingReservationId, roomId);

    }
}
