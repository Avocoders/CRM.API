namespace CRM.Data.DTO
{
    public class AccountDto
    {
        public long? Id { get; set; }
        public bool IsDeleted { get; set; }            
        public LeadDto Lead { get; set; }
        public CurrencyDto Currency { get; set; }
    }
}
