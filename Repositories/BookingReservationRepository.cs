using BusinessOjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public List<BookingReservation> GetAllBookingReservations() => BookingReservationDAO.Instance.GetAllBookingReservations();
        public void CreateBookingReservation(BookingReservation b) => BookingReservationDAO.Instance.CreateBookingReservation(b);
        public void UpdateBookingReservation(BookingReservation b) => BookingReservationDAO.Instance.UpdateBookingReservation(b);
        public void DeleteBookingReservation(BookingReservation b) => BookingReservationDAO.Instance.DeleteBookingReservation(b);
        public BookingReservation GetBookingReservation(int id) => BookingReservationDAO.Instance.GetBookingReservationById(id);
        public List<BookingReservation> GetBookingReservationsByDateRange(DateOnly startDate, DateOnly endDate) => BookingReservationDAO.Instance.GetBookingReservationsByDateRange(startDate, endDate);
    }
}
