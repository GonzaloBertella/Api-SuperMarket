using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class Shipping
    {
        public Shipping()
        {
            ShippingDetails = new HashSet<ShippingDetail>();
            ShippingPayments = new HashSet<ShippingPayment>();
        }

        public int IdShipping { get; set; }
        public int? IdShippingCompany { get; set; }
        public int? IdState { get; set; }
        public int? IdDeliveryOrder { get; set; }
        public int? IdUser { get; set; }
        public bool IsActive { get; set; }

        public virtual DeliveryOrder IdDeliveryOrderNavigation { get; set; }
        public virtual ShippingCompany IdShippingCompanyNavigation { get; set; }
        public virtual State IdStateNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }
        public virtual ICollection<ShippingPayment> ShippingPayments { get; set; }
    }
}
