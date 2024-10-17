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
    public class RoomInformationDAO : DbContext
    {
        private static readonly Lazy<RoomInformationDAO> _instance = new Lazy<RoomInformationDAO>(() => new RoomInformationDAO());
        private SqlCommand command;

        // Constructor private để ngăn việc tạo đối tượng từ bên ngoài
        private RoomInformationDAO() { }

        // Property để truy cập instance
        public static RoomInformationDAO Instance => _instance.Value;
        // Lấy tất cả thông tin phòng
        public List<RoomInformation> GetAllRooms()
        {
            List<RoomInformation> rooms = new List<RoomInformation>();
            string SQL = "SELECT RoomID, RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDay FROM RoomInformation";

            using (var command = new SqlCommand(SQL, Connection))
            {
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new RoomInformation
                            {
                                RoomId = reader.GetInt32(reader.GetOrdinal("RoomID")),
                                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                                RoomDetailDescription = reader.IsDBNull(reader.GetOrdinal("RoomDetailDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomDetailDescription")),
                                RoomMaxCapacity = reader.IsDBNull(reader.GetOrdinal("RoomMaxCapacity")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                                RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                RoomStatus = reader.IsDBNull(reader.GetOrdinal("RoomStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("RoomStatus")),
                                RoomPricePerDay = reader.IsDBNull(reader.GetOrdinal("RoomPricePerDay")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("RoomPricePerDay")),
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

            return rooms;
        }

        // Tạo một phòng mới
        public void CreateRoom(RoomInformation room)
        {
            string SQL = "INSERT INTO RoomInformation (RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDay) VALUES (@RoomNumber, @RoomDetailDescription, @RoomMaxCapacity, @RoomTypeID, @RoomStatus, @RoomPricePerDay)";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@RoomDetailDescription", room.RoomDetailDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RoomMaxCapacity", room.RoomMaxCapacity.HasValue ? (object)room.RoomMaxCapacity.Value : DBNull.Value);
                command.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeId);
                command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus.HasValue ? (object)room.RoomStatus.Value : DBNull.Value);
                command.Parameters.AddWithValue("@RoomPricePerDay", room.RoomPricePerDay.HasValue ? (object)room.RoomPricePerDay.Value : DBNull.Value);

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

        // Cập nhật thông tin phòng
        public void UpdateRoom(RoomInformation room)
        {
            string SQL = "UPDATE RoomInformation SET RoomNumber = @RoomNumber, RoomDetailDescription = @RoomDetailDescription, RoomMaxCapacity = @RoomMaxCapacity, RoomTypeID = @RoomTypeID, RoomStatus = @RoomStatus, RoomPricePerDay = @RoomPricePerDay WHERE RoomID = @RoomID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomID", room.RoomId);
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@RoomDetailDescription", room.RoomDetailDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RoomMaxCapacity", room.RoomMaxCapacity.HasValue ? (object)room.RoomMaxCapacity.Value : DBNull.Value);
                command.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeId);
                command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus.HasValue ? (object)room.RoomStatus.Value : DBNull.Value);
                command.Parameters.AddWithValue("@RoomPricePerDay", room.RoomPricePerDay.HasValue ? (object)room.RoomPricePerDay.Value : DBNull.Value);

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

        // Xóa một phòng
        public void DeleteRoom(RoomInformation room)
        {
            string SQL = "DELETE FROM RoomInformation WHERE RoomID = @RoomID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomID", room.RoomId);

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

        // Lấy thông tin phòng theo ID
        public RoomInformation GetRoomById(int roomId)
        {
            RoomInformation room = null;
            string SQL = "SELECT RoomID, RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDay FROM RoomInformation WHERE RoomID = @RoomID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomID", roomId);

                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            room = new RoomInformation
                            {
                                RoomId = reader.GetInt32(reader.GetOrdinal("RoomID")),
                                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                                RoomDetailDescription = reader.IsDBNull(reader.GetOrdinal("RoomDetailDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomDetailDescription")),
                                RoomMaxCapacity = reader.IsDBNull(reader.GetOrdinal("RoomMaxCapacity")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                                RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                RoomStatus = reader.IsDBNull(reader.GetOrdinal("RoomStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("RoomStatus")),
                                RoomPricePerDay = reader.IsDBNull(reader.GetOrdinal("RoomPricePerDay")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("RoomPricePerDay")),
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

            return room;
        }
    }
}
