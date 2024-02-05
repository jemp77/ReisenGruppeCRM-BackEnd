using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class CustomerServiceObservations
    {
        public CustomerServiceObservations()
        {
            CustomerServiceObservationFiles = new HashSet<CustomerServiceObservationFiles>();
        }

        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Observations { get; set; }
        public DateTime ObservationDate { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual ICollection<CustomerServiceObservationFiles> CustomerServiceObservationFiles { get; set; }
    }
}
