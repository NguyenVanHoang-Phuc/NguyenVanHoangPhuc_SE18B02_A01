using BusinessOjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetAllRoomTypes() => RoomTypeDAO.Instance.GetAllRoomTypes();
        public void CreateRoomType(RoomType roomType) => RoomTypeDAO.Instance.CreateRoomType(roomType);
        public void UpdateRoomType(RoomType roomType) => RoomTypeDAO.Instance.UpdateRoomType(roomType);
        public void DeleteRoomType(RoomType roomType) => RoomTypeDAO.Instance.DeleteRoomType(roomType);
        public RoomType GetRoomTypeById(int roomTypeId) => RoomTypeDAO.Instance.GetRoomTypeById(roomTypeId);
    }
}
