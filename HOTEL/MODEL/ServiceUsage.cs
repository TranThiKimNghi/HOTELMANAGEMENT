namespace HOTEL.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceUsage")]
    public partial class ServiceUsage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceUsage()
        {
            ServiceDetails = new HashSet<ServiceDetail>();
        }

        [StringLength(10)]
        public string ServiceUsageID { get; set; }

        public DateTime? ServiceUsageDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalService { get; set; }

        [StringLength(10)]
        public string PaymentID { get; set; }

        [StringLength(10)]
        public string RoomID { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Room Room { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
