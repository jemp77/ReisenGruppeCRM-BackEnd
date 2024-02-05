using SmartCardCRM.Model.Enumerables;
using System;
using System.Collections.Generic;

namespace SmartCardCRM.Model.Models
{
    public class ContractDTO
    {
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

        public ClientDTO Client { get; set; }
        public List<ContractBeneficiariesDTO> ContractBeneficiaries { get; set; }
        public List<BookingObservationsDTO> BookingObservations { get; set; }
        public ContractStatus Status { get; set; }
        public string PaymentMethods { get; set; }
        public string ContractMembershipInWords { get; set; }
        public string EntryPriceInWords { get; set; }
        public string LendInstallmentPriceInWords { get; set; }
        public string CustomDateTime { get; set; }
        public int ContractBeneficiariesCount { get; set; }
        public string ContractBeneficiariesCountInWords { get; set; }
        public byte[] ContractorSignatureBytes { get; set; }
        public byte[] CoOwnerSignatureBytes { get; set; }
        public byte[] AdviserSignature1Bytes { get; set; }
        public byte[] AdviserSignature2Bytes { get; set; }
        public byte[] AuthorizationSignatureBytes { get; set; }
    }
}
