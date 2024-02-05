using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class BookingObservations
    {
        public BookingObservations()
        {
            BookingObservationFiles = new HashSet<BookingObservationFiles>();
        }

        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Observations { get; set; }
        public DateTime ObservationDate { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual ICollection<BookingObservationFiles> BookingObservationFiles { get; set; }
    }
}
