using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL
{
    public class PaymentReport
    {
        public string BookingID { get; set; }
        public string FullName { get; set; }
        public string CitizenID { get; set; }
        public string Phone { get; set; }
        public string RoomID { get; set; }
        public string BasePrice { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }
        public string RoomType { get; set; }
        public string ServiceName { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string TotalService { get; set; }
        public string TotalAmount { get; set; }
        public string CustomerID { get; set; }
    }
}
