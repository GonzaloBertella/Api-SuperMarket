using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class ShippingCompany
    {
        public ShippingCompany()
        {
            Shippings = new HashSet<Shipping>();
        }

        public int IdShippingCompany { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Cuit { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public int? IdShippingType { get; set; }
        public bool IsActive { get; set; }
        public double? Salary { get; set; }
        public string ContactName { get; set; }
        public int? MaxShippingsPerDay { get; set; }

        public virtual ShippingType IdShippingTypeNavigation { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
