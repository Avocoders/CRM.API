namespace CRM.Data.DTO
{
    public class AccountDto
    {
        public long? Id { get; set; }  
        public long LeadId { get; set; }
        public byte CurrencyId { get; set; }
        public bool IsDeleted { get; set; }
        public LeadDto Lead { get; set; }
    }
}
