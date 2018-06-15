using System;

namespace VirtualOfficeCloud.Data.Models.BaseEntity
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
