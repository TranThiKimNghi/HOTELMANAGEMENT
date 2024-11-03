namespace HOTEL.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        HotelContextDB db  = new HotelContextDB();
        public bool CheckLogin(string username, string password)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                return user != null;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
