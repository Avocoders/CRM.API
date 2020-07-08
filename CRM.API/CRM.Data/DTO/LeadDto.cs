using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class LeadDto
    {
        public long? Id { get; set; }
        public byte? RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
        
        // объеденить параметры Role и City в DTO
        // модифицировать методы 
    }
}
