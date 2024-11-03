using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTELMANAGEMENT.BUS
{
    public class BookingDetailSevice
    {
        private readonly HotelContextDB context = new HotelContextDB();
        public List<BookingDetail> GetAll()
        {
            HotelContextDB context = new HotelContextDB();
            return context.BookingDetails.ToList();

        }
    }
}
