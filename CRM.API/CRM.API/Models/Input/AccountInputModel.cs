namespace CRM.API.Models.Input
{
    public class AccountInputModel
    {
		public long? Id { get; set; }
		public long LeadId { get; set; }		
		public byte? CurrencyId { get; set; }
    }
}
