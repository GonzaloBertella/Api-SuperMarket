using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class Pickup
    {
        public Pickup()
        {
            PickupDetails = new HashSet<PickupDetail>();
        }

        public int IdPickup { get; set; }
        public int? IdDeliveryOrder { get; set; }
        public int? IdState { get; set; }
        public int? IdUser { get; set; }
        public bool? IsActive { get; set; }

        public virtual DeliveryOrder IdDeliveryOrderNavigation { get; set; }
        public virtual State IdStateNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<PickupDetail> PickupDetails { get; set; }
    }
}