using System;

namespace CRM.Data
{
    public class LeadSearchParameters
    {        
        public int? RoleId { get; set; }
		public int? FirstNameSearchMode { get; set; }
		public string FirstName { get; set; }
		public int? LastNameSearchMode { get; set; }
		public string LastName { get; set; }
		public int? PatronymicSearchMode { get; set; }
		public string Patronymic { get; set; }
		public int? LoginSearchMode { get; set; }
		public string Login { get; set; }
		public int? PhoneSearchMode { get; set; }
		public string Phone { get; set; }
		public int? EmailSearchMode { get; set; }
		public string Email { get; set; }
        public int? CityId { get; set; }
		public int? AddressSearchMode { get; set; }
		public string Address { get; set; }
        public DateTime? BirthDateBegin { get; set; }
		public DateTime? BirthDateEnd { get; set; }
		public DateTime? RegistrationDateBegin { get; set; }
		public DateTime? RegistrationDateEnd { get; set; }
		public long? AccountId { get; set; }
		public byte CurrencyId { get; set; }
		public byte? IncludeDeleted { get; set; }
    }
}
