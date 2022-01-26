using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class PickupDetail
    {
        public int IdPickupDetail { get; set; }
        public int? IdPickup { get; set; }
        public string Weight { get; set; }
        public string Volume { get; set; }
        public int? BagsQuantity { get; set; }

        public virtual Pickup IdPickupNavigation { get; set; }
    }
}
