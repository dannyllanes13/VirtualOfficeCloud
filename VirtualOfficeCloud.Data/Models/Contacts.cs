using VirtualOfficeCloud.Data.Models.BaseEntity;

namespace VirtualOfficeCloud.Data.Models
{
    public class Contacts : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FamiliarName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Phone { get; set; }
        public string SinSsn { get; set; }

    }
}
