using System;
using System.Collections.Generic;

#nullable disable

using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class DeliveryOrder
    {
        public DeliveryOrder()
        {
            Pickups = new HashSet<Pickup>();
            Shippings = new HashSet<Shipping>();
        }

        public int IdDeliveryOrder { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int? IdZone { get; set; }
        public int? IdBranch { get; set; }
        public bool? IsOwner { get; set; }
        public bool? IsShipping { get; set; }
        public double? ShippingPrice { get; set; }
        public bool? IsFree { get; set; }

        public virtual Branch IdBranchNavigation { get; set; }
        public virtual Zone IdZoneNavigation { get; set; }
        public virtual ICollection<Pickup> Pickups { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
