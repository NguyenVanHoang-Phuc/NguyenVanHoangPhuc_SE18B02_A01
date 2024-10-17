using BusinessOjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
        List<RoomInformation> GetAllRooms();
        void CreateRoom(RoomInformation room);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(RoomInformation room);
        RoomInformation GetRoom(int id);
    }
}
