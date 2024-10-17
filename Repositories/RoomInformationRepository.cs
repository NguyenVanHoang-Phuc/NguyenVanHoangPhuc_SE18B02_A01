using BusinessOjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public List<RoomInformation> GetAllRooms() => RoomInformationDAO.Instance.GetAllRooms();
        public void CreateRoom(RoomInformation room) => RoomInformationDAO.Instance.CreateRoom(room);
        public void UpdateRoom(RoomInformation room) => RoomInformationDAO.Instance.UpdateRoom(room);
        public void DeleteRoom(RoomInformation room) => RoomInformationDAO.Instance.DeleteRoom(room);
        public RoomInformation GetRoom(int id) => RoomInformationDAO.Instance.GetRoomById(id);
    }
}
