using System;
using System.Collections.Generic;

#nullable disable

namespace SuperMamiApi.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Users = new HashSet<User>();
        }

        public int IdDocumentType { get; set; }
        public string DocumentType1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
