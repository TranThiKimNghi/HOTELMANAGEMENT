using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTELMANAGEMENT.BUS.model
{
    public class BookingService
    {
        private readonly HotelContextDB context = new HotelContextDB();
        public List<Booking> GetAll()
        {
            HotelContextDB context = new HotelContextDB();
            return context.Bookings.ToList();

        }
    }
}
