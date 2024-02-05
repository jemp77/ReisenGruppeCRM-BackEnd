using System;
using System.Collections.Generic;

namespace SmartCardCRM.Data.Entities
{
    public partial class Room
    {
        public int Id { get; set; }
        public int IdQuoter { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public string KidsAges { get; set; }

        public virtual Quoter IdQuoterNavigation { get; set; }
    }
}
