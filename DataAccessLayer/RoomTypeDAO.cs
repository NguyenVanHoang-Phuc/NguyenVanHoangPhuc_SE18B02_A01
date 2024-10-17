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
    public class RoomTypeDAO : DbContext
    {
        private static readonly Lazy<RoomTypeDAO> _instance = new Lazy<RoomTypeDAO>(() => new RoomTypeDAO());
        private SqlCommand command;

        // Constructor private để ngăn việc tạo đối tượng từ bên ngoài
        private RoomTypeDAO() { }

        // Property để truy cập instance
        public static RoomTypeDAO Instance => _instance.Value;
        // Lấy tất cả loại phòng
        public List<RoomType> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = new List<RoomType>();
            string SQL = "SELECT RoomTypeID, RoomTypeName, TypeDescription, TypeNote FROM RoomType";

            using (var command = new SqlCommand(SQL, Connection))
            {
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            roomTypes.Add(new RoomType
                            {
                                RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                RoomTypeName = reader.GetString(reader.GetOrdinal("RoomTypeName")),
                                TypeDescription = reader.IsDBNull(reader.GetOrdinal("TypeDescription")) ? null : reader.GetString(reader.GetOrdinal("TypeDescription")),
                                TypeNote = reader.IsDBNull(reader.GetOrdinal("TypeNote")) ? null : reader.GetString(reader.GetOrdinal("TypeNote")),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi lấy danh sách loại phòng: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return roomTypes;
        }

        // Tạo một loại phòng mới
        public void CreateRoomType(RoomType roomType)
        {
            string SQL = "INSERT INTO RoomType (RoomTypeName, TypeDescription, TypeNote) VALUES (@RoomTypeName, @TypeDescription, @TypeNote)";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomTypeName", roomType.RoomTypeName);
                command.Parameters.AddWithValue("@TypeDescription", roomType.TypeDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TypeNote", roomType.TypeNote ?? (object)DBNull.Value);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tạo loại phòng: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Cập nhật thông tin loại phòng
        public void UpdateRoomType(RoomType roomType)
        {
            string SQL = "UPDATE RoomType SET RoomTypeName = @RoomTypeName, TypeDescription = @TypeDescription, TypeNote = @TypeNote WHERE RoomTypeID = @RoomTypeID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeId);
                command.Parameters.AddWithValue("@RoomTypeName", roomType.RoomTypeName);
                command.Parameters.AddWithValue("@TypeDescription", roomType.TypeDescription ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TypeNote", roomType.TypeNote ?? (object)DBNull.Value);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật loại phòng: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Xóa một loại phòng
        public void DeleteRoomType(RoomType roomType)

        {
            string SQL = "DELETE FROM RoomType WHERE RoomTypeID = @RoomTypeID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeId);

                try
                {
                    OpenConnection();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi xóa loại phòng: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        // Lấy thông tin loại phòng theo ID
        public RoomType GetRoomTypeById(int roomTypeId)
        {
            RoomType roomType = null;
            string SQL = "SELECT RoomTypeID, RoomTypeName, TypeDescription, TypeNote FROM RoomType WHERE RoomTypeID = @RoomTypeID";

            using (var command = new SqlCommand(SQL, Connection))
            {
                command.Parameters.AddWithValue("@RoomTypeID", roomTypeId);

                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            roomType = new RoomType
                            {
                                RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                RoomTypeName = reader.GetString(reader.GetOrdinal("RoomTypeName")),
                                TypeDescription = reader.IsDBNull(reader.GetOrdinal("TypeDescription")) ? null : reader.GetString(reader.GetOrdinal("TypeDescription")),
                                TypeNote = reader.IsDBNull(reader.GetOrdinal("TypeNote")) ? null : reader.GetString(reader.GetOrdinal("TypeNote")),
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi lấy thông tin loại phòng: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }

            return roomType;
        }

    }
}
