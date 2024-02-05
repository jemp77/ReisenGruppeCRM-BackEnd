using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class CustomerServiceObservationFiles
    {
        public int Id { get; set; }
        public int CustomerServiceObservationId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public virtual CustomerServiceObservations CustomerServiceObservation { get; set; }
    }
}
