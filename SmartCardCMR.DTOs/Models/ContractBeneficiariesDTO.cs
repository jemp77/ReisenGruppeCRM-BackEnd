using System;

namespace SmartCardCRM.Model.Models
{
    public class ContractBeneficiariesDTO
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int ContractId { get; set; }
    }
}
