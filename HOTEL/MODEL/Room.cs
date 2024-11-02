namespace HOTEL.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            BookingDetails = new HashSet<BookingDetail>();
            ServiceUsages = new HashSet<ServiceUsage>();
        }

        [StringLength(10)]
        public string RoomID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; }

        [StringLength(20)]
        public string RoomStatus { get; set; }

        public int Capacity { get; set; }

        public decimal BasePrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceUsage> ServiceUsages { get; set; }
    }
}
