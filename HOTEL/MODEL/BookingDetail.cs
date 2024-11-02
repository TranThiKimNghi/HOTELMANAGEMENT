namespace HOTEL.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BookingDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string BookingID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string RoomID { get; set; }

        public decimal RoomPrice { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Room Room { get; set; }
    }
}
