using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class User
    {
        public User()
        {
            Pickups = new HashSet<Pickup>();
            Shippings = new HashSet<Shipping>();
        }

        public int IdUser { get; set; }
        public int? IdDocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? IdRol { get; set; }
        public bool? IsActive { get; set; }
        public string Password { get; set; }

        public virtual DocumentType IdDocumentTypeNavigation { get; set; }
        public virtual Role IdRolNavigation { get; set; }
        public virtual ICollection<Pickup> Pickups { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
