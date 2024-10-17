using BusinessOjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        List<BookingReservation> GetAllBookingReservations();
        void CreateBookingReservation(BookingReservation b);
        void UpdateBookingReservation(BookingReservation b);
        void DeleteBookingReservation(BookingReservation b);
        BookingReservation GetBookingReservation(int id);
        List<BookingReservation> GetBookingReservationsByDateRange(DateOnly startDate, DateOnly endDate);
    }
}
