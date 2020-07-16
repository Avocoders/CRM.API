using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Input
{
    public class SearchParametersInputModel
    {
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string RegistrationDate { get; set; }
        public byte IsDeleted { get; set; }
    }
}
