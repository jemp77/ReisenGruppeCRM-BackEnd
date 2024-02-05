using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
