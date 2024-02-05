using System;
using System.Collections.Generic;

namespace SmartCardCRM.Model.Models
{
    public class BookingObservationsDTO
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Observations { get; set; }
        public DateTime ObservationDate { get; set; }

        public List<BookingObservationFilesDTO> BookingObservationFiles { get; set; }
    }
}
