using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HOTELMANAGEMENT.BUS.model
{
    public class CustomerService
    {
        private readonly HotelContextDB context = new HotelContextDB();
        public List<Customer> GetAll()
        {
            HotelContextDB context = new HotelContextDB();
            return context.Customers.ToList();

        }
        public Customer GetCustomerById(string CustomerId)
        {
            return context.Customers.FirstOrDefault(s => s.CustomerID == CustomerId);
        }
        public string DeleteCustomer(string CustomerId)
        {
            try
            {
                var existingCustomrer = context.Customers.FirstOrDefault(s => s.CustomerID == CustomerId);
                if (existingCustomrer == null)
                {
                    return "khách hàng không tồn tại.";
                }

                context.Customers.Remove(existingCustomrer);
                context.SaveChanges();
                return "Xóa khách hàng thành công!";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi xóa khách hàng: {ex.Message}";
            }
        }
        public string UpdateCustomeer(Customer customer)
        {
            try
            {
                var existingcustomer = context.Customers.FirstOrDefault(s => s.CustomerID == customer.CustomerID);
                if (existingcustomer == null)
                {
                    return "khách hàng không tồn tại.";
                }

                existingcustomer.CustomerID = customer.CustomerID;
                existingcustomer.FullName = customer.FullName;
                existingcustomer.CitizenID = customer.CitizenID;
                existingcustomer.Phone = customer.Phone;

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
