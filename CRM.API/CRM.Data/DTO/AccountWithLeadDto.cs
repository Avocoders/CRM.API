using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class AccountWithLeadDto
    {
        public long? AccountId { get; set; }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }      
        public string CityName { get; set; }
        public bool IsDeleted { get; set; }
        public byte CurrencyId { get; set; }
        public bool StateAccount { get; set; }
    }
}
