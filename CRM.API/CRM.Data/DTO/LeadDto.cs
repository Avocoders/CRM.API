using System;

namespace CRM.Data.DTO
{
    public class LeadDto
    {
        public long? Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }        
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public RoleDto Role { get; set; }
        public CityDto City { get; set; }
    }
}
