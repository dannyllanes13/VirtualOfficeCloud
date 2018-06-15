using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VirtualOfficeCloud.Data.Models
{
    public class StoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("PersonId")]
        public virtual Persons Person { get; set; }
    }
}
