namespace HOTEL.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ServiceUsageID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ServiceID { get; set; }

        public int Quantity { get; set; }

        public virtual ServiceUsage ServiceUsage { get; set; }

        public virtual Service Service { get; set; }
    }
}
