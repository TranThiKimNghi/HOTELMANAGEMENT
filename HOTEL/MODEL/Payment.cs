namespace HOTEL.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            ServiceUsages = new HashSet<ServiceUsage>();
        }

        [StringLength(10)]
        public string PaymentID { get; set; }

        [StringLength(10)]
        public string BookingID { get; set; }

        public DateTime? PaymentDate { get; set; }

        public virtual Booking Booking { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceUsage> ServiceUsages { get; set; }
    }
}
