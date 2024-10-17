using BusinessOjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetAllRoomTypes();
        void CreateRoomType(RoomType roomType);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(RoomType roomType);
        RoomType GetRoomTypeById(int roomTypeId);
    }
}
