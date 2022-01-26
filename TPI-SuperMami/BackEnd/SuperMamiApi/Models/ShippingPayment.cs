using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class ShippingPayment
    {
        public int IdShippingPayment { get; set; }
        public int? IdShipping { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public virtual Shipping IdShippingNavigation { get; set; }
    }
}
