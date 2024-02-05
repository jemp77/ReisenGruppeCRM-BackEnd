using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class Contract
    {
        public Contract()
        {
            BookingObservations = new HashSet<BookingObservations>();
            ContractBeneficiaries = new HashSet<ContractBeneficiaries>();
            CustomerServiceObservations = new HashSet<CustomerServiceObservations>();
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime ContractDate { get; set; }
        public string ContractNumber { get; set; }
        public long MembershipPrice { get; set; }
        public long EntryPrice { get; set; }
        public long CuiPrice { get; set; }
        public long? LendValue { get; set; }
        public short? LendInstallments { get; set; }
        public long? LendInstallmentPrice { get; set; }
        public DateTime? LendInstallmentDate { get; set; }
        public short DurationYears { get; set; }
        public string Observations { get; set; }
        public string ContractorSignature { get; set; }
        public string CoOwnerSignature { get; set; }
        public string Adviser1Signature { get; set; }
        public string Adviser2Signature { get; set; }
        public string AuthorizationSignature { get; set; }
        public long? DebitCardPayment { get; set; }
        public long? CreditCardPayment { get; set; }
        public long? TransferPayment { get; set; }
        public long? CashPayment { get; set; }
        public string Status { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<BookingObservations> BookingObservations { get; set; }
        public virtual ICollection<ContractBeneficiaries> ContractBeneficiaries { get; set; }
        public virtual ICollection<CustomerServiceObservations> CustomerServiceObservations { get; set; }
    }
}
