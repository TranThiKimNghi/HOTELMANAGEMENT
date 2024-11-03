using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTELMANAGEMENT.BUS.model
{
    public class serviceServic
    {
        private readonly HotelContextDB context = new HotelContextDB();
        public List<Service> GetAll()
        {
            HotelContextDB context = new HotelContextDB();
            return context.Services.ToList();

        }
        public Service GetServiceById(string servicerID)
        {
            return context.Services.FirstOrDefault(s => s.ServiceID == servicerID);
        }
        public string DeleteService(string servicerID)
        {
            try
            {
                var existingService = context.Services.FirstOrDefault(s => s.ServiceID == servicerID);
                if (existingService == null)
                {
                    return "";
                }

                context.Services.Remove(existingService);
                context.SaveChanges();
                return "Xóa dịch vụ  thành công!";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi xóa dịch vụ : {ex.Message}";
            }
        }
        public string UpdateService(Service Service)
        {
            try
            {
                var existingService = context.Services.FirstOrDefault(s => s.ServiceID == Service.ServiceID);
                if (existingService == null)
                {
                    return "";
                }

                existingService.ServiceID = Service.ServiceID;
                existingService.ServiceName = Service.ServiceName;
               // existingService.q = customer.CitizenID;
                existingService.Price = Service.Price;

                context.SaveChanges();
                return "Cập nhật thông tin khách hàng thành công!";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi cập nhật khách hàng: {ex.Message}";
            }
        }
    }
}
