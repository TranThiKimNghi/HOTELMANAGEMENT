using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HOTELMANAGEMENT.BUS.model
{
    public class ServiceDetailService
    {
        HotelContextDB db = new HotelContextDB();
        public List<ServiceDetail> GetAll()
        {

            return db.ServiceDetails.ToList();
        }

        public ServiceDetail GetServiceDetailById(string SevicerDetailID)
        {
            return db.ServiceDetails.FirstOrDefault(s => s.ServiceID == SevicerDetailID);
        }
    }
}
