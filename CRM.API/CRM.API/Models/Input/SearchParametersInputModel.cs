namespace CRM.API.Models.Input
{
    public class SearchParametersInputModel
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
		public string BirthDateBegin { get; set; }
		public string BirthDateEnd { get; set; }
		public string RegistrationDateBegin { get; set; }
		public string RegistrationDateEnd { get; set; }
		public byte? IncludeDeleted { get; set; }
	}
}
