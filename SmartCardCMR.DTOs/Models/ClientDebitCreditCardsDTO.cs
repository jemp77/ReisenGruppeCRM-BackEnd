namespace SmartCardCRM.Model.Models
{
    public class ClientDebitCreditCardsDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool IsClientCard { get; set; }
        public string CardType { get; set; }
        public string FranchiseName { get; set; }
        public string BankName { get; set; }
    }
}
