using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTELMANAGEMENT.BUS.model
{
    public class UserService
    {
        private User User = new User();

        public bool Login(string username, string password)
        {
            return User.CheckLogin(username, password);
        }
    }
}
