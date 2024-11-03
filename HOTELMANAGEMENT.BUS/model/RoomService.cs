using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTELMANAGEMENT.BUS.model
{
    public class RoomService
    {
        private readonly HotelContextDB context = new HotelContextDB();
        public List<Room> GetAll()
        {
            HotelContextDB context = new HotelContextDB();
            return context.Rooms.ToList();

        }
        public Room GetRoomById(string RoomId)
        {
            return context.Rooms.FirstOrDefault(s => s.RoomID == RoomId);
        }
        public string DeleteRoom(string RoomId)
        {
            try
            {
                var existingRoom = context.Rooms.FirstOrDefault(s => s.RoomID == RoomId);
                if (existingRoom == null)
                {
                    return "Phòng không tồn tại.";
                }

                context.Rooms.Remove(existingRoom);
                context.SaveChanges();
                return "Xóa phòng thành công!";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi xóa phòng: {ex.Message}";
            }
        }
    }
}
