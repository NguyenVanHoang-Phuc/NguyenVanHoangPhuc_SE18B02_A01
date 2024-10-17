using BusinessOjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingReservationDAO : DbContext
    {
        private static readonly Lazy<BookingReservationDAO> _instance = new Lazy<BookingReservationDAO>(() => new BookingReservationDAO());
        private SqlCommand command;

        // Constructor private để ngăn việc tạo đối tượng từ bên ngoài
        private BookingReservationDAO() { }

        // Property để truy cập instance
        public static BookingReservationDAO Instance => _instance.Value;
        
        // Lấy tất cả các đặt phòng
        public List<BookingReservation> GetAllBookingReservations()
        {
            List<BookingReservation> reservations = new List<BookingReservation>();
            string SQL = "SELECT BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus FROM BookingReservation";

            using (var command = new SqlCommand(SQL, Connection))
            {
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            reservations.Add(new BookingReservation
                            {
                                BookingReservationId = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                BookingDate = reader.IsDBNull(reader.GetOrdinal("BookingDate")) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BookingDate"))),
                                TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                BookingStatus = reader.IsDBNull(reader.GetOrdinal("BookingStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("BookingStatus"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return reservations;
        }

        // Thêm một đặt phòng mới
        public void CreateBookingReservation(BookingReservation b)
        {
            string SQL = "INSERT INTO BookingReservation (BookingDate, TotalPrice, CustomerID, BookingStatus) VALUES (@BookingDate, @TotalPrice, @CustomerID, @BookingStatus)";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingDate", b.BookingDate.HasValue ? (object)b.BookingDate.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                command.Parameters.AddWithValue("@TotalPrice", b.TotalPrice.HasValue ? (object)b.TotalPrice.Value : DBNull.Value);
                command.Parameters.AddWithValue("@CustomerID", b.CustomerId);
                command.Parameters.AddWithValue("@BookingStatus", b.BookingStatus.HasValue ? (object)b.BookingStatus.Value : DBNull.Value);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Cập nhật thông tin đặt phòng
        public void UpdateBookingReservation(BookingReservation b)
        {
            string SQL = "UPDATE BookingReservation SET BookingDate = @BookingDate, TotalPrice = @TotalPrice, CustomerID = @CustomerID, BookingStatus = @BookingStatus WHERE BookingReservationID = @BookingReservationID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", b.BookingReservationId);
                command.Parameters.AddWithValue("@BookingDate", b.BookingDate.HasValue ? (object)b.BookingDate.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                command.Parameters.AddWithValue("@TotalPrice", b.TotalPrice.HasValue ? (object)b.TotalPrice.Value : DBNull.Value);
                command.Parameters.AddWithValue("@CustomerID", b.CustomerId);
                command.Parameters.AddWithValue("@BookingStatus", b.BookingStatus.HasValue ? (object)b.BookingStatus.Value : DBNull.Value);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Xóa một đặt phòng
        public void DeleteBookingReservation(BookingReservation b)
        {
            string SQL = "DELETE FROM BookingReservation WHERE BookingReservationID = @BookingReservationID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", b.BookingReservationId);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Lấy thông tin đặt phòng theo ID
        public BookingReservation GetBookingReservationById(int bookingReservationId)
        {
            BookingReservation reservation = null;
            string SQL = "SELECT BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus FROM BookingReservation WHERE BookingReservationID = @BookingReservationID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", bookingReservationId);

                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            reservation = new BookingReservation
                            {
                                BookingReservationId = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                BookingDate = reader.IsDBNull(reader.GetOrdinal("BookingDate")) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BookingDate"))),
                                TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                BookingStatus = reader.IsDBNull(reader.GetOrdinal("BookingStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("BookingStatus"))
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return reservation;
        }

        // Lấy danh sách đặt phòng theo khoảng thời gian
        public List<BookingReservation> GetBookingReservationsByDateRange(DateOnly startDate, DateOnly endDate)
        {
            List<BookingReservation> reservations = GetAllBookingReservations(); // Lấy tất cả đặt phòng

            // Sử dụng LINQ để lọc danh sách theo khoảng thời gian
            return reservations
                .Where(r => r.BookingDate >= startDate && r.BookingDate <= endDate)
                .ToList();
        }
    }
}
