namespace CRM.API.Models.Output
{
    public class AccountOutputModel
    {
		public long Id { get; set; }
		public bool IsDeleted { get; set; }		
		public string CurrencyCode { get; set; }
		public string CurrencyName { get; set; }
	}
}
