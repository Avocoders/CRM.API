namespace CRM.Data.DTO
{
    public class AccountDto
    {
        public long? Id { get; set; }
        public bool IsDeleted { get; set; }            
        
        public byte CurrencyId { get; set; }
    }
}
