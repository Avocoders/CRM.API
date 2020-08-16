using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Output
{
    public class AccountWithLeadOutputModel
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string CityName { get; set; }
        public bool IsDeleted { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public bool StateAccount { get; set; }
    }
}
