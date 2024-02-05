using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class ClientDebitCreditCards
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool IsClientCard { get; set; }
        public string CardType { get; set; }
        public string FranchiseName { get; set; }
        public string BankName { get; set; }

        public virtual Client Client { get; set; }
    }
}
