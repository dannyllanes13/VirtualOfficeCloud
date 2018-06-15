using System;
using VirtualOfficeCloud.Data.Models.BaseEntity;

namespace VirtualOfficeCloud.Data.Models
{
    public class Persons : Entity
    {
        public string AccessLevel { get; set; }
        public DateTime? LastLoggedIn { get; set; }

        public int ContactId { get; set; }
        public Contacts Contact { get; set; }
    }
}
