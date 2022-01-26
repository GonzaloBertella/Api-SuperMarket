using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class ShippingDetail
    {
        public int IdShippingDetail { get; set; }
        public int? IdShipping { get; set; }
        public string Old { get; set; }
        public string Comment { get; set; }
        public int? Weight { get; set; }

        public virtual Shipping IdShippingNavigation { get; set; }
    }
}
