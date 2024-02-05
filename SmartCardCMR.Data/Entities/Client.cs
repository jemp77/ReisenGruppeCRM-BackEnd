using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartCardCRM.Data.Entities
{
    public partial class Client
    {
        public Client()
        {
            ClientDebitCreditCards = new HashSet<ClientDebitCreditCards>();
            Contract = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public short? Age { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Office { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long? MonthlyIncome { get; set; }
        public bool? HasWorkedWithTourismIndustry { get; set; }
        public string TourismIndustry { get; set; }
        public string DebitCreditBanks { get; set; }
        public string CoOwnerName { get; set; }
        public string CoOwnerLastName { get; set; }
        public string CoOwnerGender { get; set; }
        public string CoOwnerProfession { get; set; }
        public short? CoOwnerAge { get; set; }
        public string CoOwnerDocumentType { get; set; }
        public string CoOwnerDocumentNumber { get; set; }
        public string CoOwnerAddress { get; set; }
        public string CoOwnerCellPhone { get; set; }
        public string CoOwnerOffice { get; set; }
        public string CoOwnerCity { get; set; }
        public string CoOwnerDepartment { get; set; }
        public string CoOwnerCountry { get; set; }
        public DateTime? CoOwnerBirthDate { get; set; }
        public string CoOwnerPhoneNumber { get; set; }
        public string CoOwnerEmail { get; set; }
        public long? CoOwnerMonthlyIncome { get; set; }
        public bool? CoOwnerHasWorkedWithTourismIndustry { get; set; }
        public string CoOwnerTourismIndustry { get; set; }
        public string CoOwnerDebitCreditBanks { get; set; }
        public string MaritalStatus { get; set; }
        public short? Sons { get; set; }
        public string HousingType { get; set; }
        public string Neighborhood { get; set; }
        public bool? HasCar { get; set; }
        public string CarBrandModel { get; set; }
        public string Linner { get; set; }
        public string Closer { get; set; }
        public string TlmkCode { get; set; }
        public DateTime Date { get; set; }
        public short? TableNumber { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan? WayOutTime { get; set; }
        public string Signature { get; set; }
        public string Voucher { get; set; }
        public string Observations { get; set; }
        public bool? AttemptSell { get; set; }
        public bool? CreditDenied { get; set; }
        public bool? Reserve { get; set; }
        public long? ReserveValue { get; set; }

        public virtual ICollection<ClientDebitCreditCards> ClientDebitCreditCards { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
    }
}
