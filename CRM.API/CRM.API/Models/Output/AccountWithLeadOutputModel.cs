using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Models.Output
{
    public class AccountWithLeadOutputModel
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public bool LeadIsDeleted { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public bool IsDeleted { get; set; }


        //public override bool Equals(object obj)
        //{
        //    AccountWithLeadOutputModel lom = (AccountWithLeadOutputModel)obj;
        //    if (Id.Value == lom.Id.Value &&
        //        FirstName == lom.FirstName &&
        //        LastName == lom.LastName &&
        //        Patronymic == lom.Patronymic &&
        //        Phone == lom.Phone &&
        //        Address == lom.Address &&
        //        BirthDate == lom.BirthDate &&
        //        CityName == lom.CityName &&
        //        IsDeleted == lom.IsDeleted &&
        //        CurrencyCode == lom.CurrencyCode &&
        //        CurrencyName == lom.CurrencyName &&
        //        StateAccount == lom.StateAccount)
        //        return true;

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

    }

}
