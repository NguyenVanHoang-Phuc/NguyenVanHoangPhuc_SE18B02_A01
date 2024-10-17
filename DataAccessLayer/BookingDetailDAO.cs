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
    public class BookingDetailDAO : DbContext
    {
        private static readonly Lazy<BookingDetailDAO> _instance = new Lazy<BookingDetailDAO>(() => new BookingDetailDAO());
        private SqlCommand command;

        // Constructor private để ngăn việc tạo đối tượng từ bên ngoài
        private BookingDetailDAO() { }

        // Property để truy cập instance
        public static BookingDetailDAO Instance => _instance.Value;
        // Lấy list
        public List<BookingDetail> GetBookingDetails()
        {
            List<BookingDetail> listBookingDetail = new List<BookingDetail>();
            string SQL = "SELECT BookingID, CustomerID, RoomID, StartDate, EndDate, Status FROM BookingDetails"; // Adjust SQL to match your table structure.

            using (command = new SqlCommand(SQL, Connection))
            {
                try
                {
                    OpenConnection();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            listBookingDetail.Add(new BookingDetail
                            {
                                BookingReservationId = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                RoomId = reader.GetInt32(reader.GetOrdinal("RoomID")),
                                StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
                                EndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))),
                                ActualPrice = reader.IsDBNull(reader.GetOrdinal("ActualPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualPrice"))
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
                return listBookingDetail;
            }
        }

        // Tạo mới
        public void CreateBookingDetail(BookingDetail b)
        {
            string SQL = "INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDate, EndDate, ActualPrice) VALUES (@BookingReservationID, @RoomID, @StartDate, @EndDate, @ActualPrice)";

            using (SqlCommand command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", b.BookingReservationId);
                command.Parameters.AddWithValue("@RoomID", b.RoomId);
                command.Parameters.AddWithValue("@StartDate", b.StartDate.ToDateTime(new TimeOnly()));
                command.Parameters.AddWithValue("@EndDate", b.EndDate.ToDateTime(new TimeOnly()));
                command.Parameters.AddWithValue("@ActualPrice", b.ActualPrice.HasValue ? (object)b.ActualPrice.Value : DBNull.Value);

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

        // Cập nhật
        public void UpdateBookingDetail(BookingDetail b)
        {
            string SQL = "UPDATE BookingDetail SET RoomID = @RoomID, StartDate = @StartDate, EndDate = @EndDate, ActualPrice = @ActualPrice WHERE BookingReservationID = @BookingReservationID";

            using (SqlCommand command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", b.BookingReservationId);
                command.Parameters.AddWithValue("@RoomID", b.RoomId);
                command.Parameters.AddWithValue("@StartDate", b.StartDate.ToDateTime(new TimeOnly()));
                command.Parameters.AddWithValue("@EndDate", b.EndDate.ToDateTime(new TimeOnly()));
                command.Parameters.AddWithValue("@ActualPrice", b.ActualPrice.HasValue ? (object)b.ActualPrice.Value : DBNull.Value);

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

        // Xóa
        public void DeleteBookingDetail(BookingDetail b)
        {
            string SQL = "DELETE FROM BookingDetail WHERE BookingReservationID = @BookingReservationID";

            using (SqlCommand command = new SqlCommand(SQL, Connection))
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

        public BookingDetail GetBookingDetailById(int bookingReservationId, int roomId)
        {
            BookingDetail bookingDetail = null;
            string SQL = "SELECT BookingReservationID, RoomID, StartDate, EndDate, ActualPrice FROM BookingDetail WHERE BookingReservationID = @BookingReservationID AND RoomID = @RoomID";

            using (SqlCommand command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", bookingReservationId);
                command.Parameters.AddWithValue("@RoomID", roomId);

                try
                {
                    OpenConnection();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            bookingDetail = new BookingDetail
                            {
                                BookingReservationId = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                RoomId = reader.GetInt32(reader.GetOrdinal("RoomID")),
                                StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
                                EndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))),
                                ActualPrice = reader.IsDBNull(reader.GetOrdinal("ActualPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualPrice"))
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

            return bookingDetail;
        }
    }
}
        

