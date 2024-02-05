using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class BookingObservationFiles
    {
        public int Id { get; set; }
        public int BookingObservationId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public virtual BookingObservations BookingObservation { get; set; }
    }
}
