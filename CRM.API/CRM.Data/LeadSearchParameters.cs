﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data
{
    public class LeadSearchParameters
    {
        //public long Id { get; set; }
        public int? RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }       
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        //public DateTime ChangeDate { get; set; }
        public bool? IncludeDeleted { get; set; }
    }
}
