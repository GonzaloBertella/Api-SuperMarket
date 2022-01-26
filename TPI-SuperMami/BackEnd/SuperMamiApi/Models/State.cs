using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class State
    {
        public State()
        {
            Pickups = new HashSet<Pickup>();
            Shippings = new HashSet<Shipping>();
        }

        public int IdState { get; set; }
        public string State1 { get; set; }

        public virtual ICollection<Pickup> Pickups { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
