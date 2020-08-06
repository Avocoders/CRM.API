using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.API.Models.Output
{
    public class LeadWithAccountsOutputModel
    {
        public long Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public string BirthDate { get; set; }
       
        public List<AccountOutputModel> Accounts { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            LeadOutputModel lom = (LeadOutputModel)obj;
            if (Id == lom.Id &&
                FirstName == lom.FirstName &&
                LastName == lom.LastName &&
                BirthDate == lom.BirthDate &&
                IsDeleted == lom.IsDeleted)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
