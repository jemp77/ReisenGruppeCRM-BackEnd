using System;
using System.Collections.Generic;

namespace SmartCardCRM.Model.Models
{
    public class CustomerServiceObservationsDTO
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Observations { get; set; }
        public DateTime ObservationDate { get; set; }

        public virtual List<CustomerServiceObservationFilesDTO> CustomerServiceObservationFiles { get; set; }
    }
}
