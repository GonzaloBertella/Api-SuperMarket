using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class ShippingType
    {
        public ShippingType()
        {
            ShippingCompanies = new HashSet<ShippingCompany>();
        }

        public int IdShippingType { get; set; }
        public string Description { get; set; }
        public string MaxWeightCapacity { get; set; }
        public string MaxVolumeCapacity { get; set; }
        public int? MaxBagsCapacity { get; set; }

        public virtual ICollection<ShippingCompany> ShippingCompanies { get; set; }
    }
}
