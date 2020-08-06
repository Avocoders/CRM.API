namespace CRM.API.Models.Output
{
    public class AccountOutputModel
    {
		public long Id { get; set; }
		public bool IsDeleted { get; set; }
		public long LeadId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string BirthDate { get; set; }
		public byte CurrencyId { get; set; } 
	}
}
