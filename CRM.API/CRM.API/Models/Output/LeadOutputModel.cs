using System.Collections.Generic;

namespace CRM.API.Models.Output
{
    public class LeadOutputModel
    {
        public long Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }        
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string RegistrationDate { get; set; }
        public string ChangeDate { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
        public List<AccountOutputModel> Accounts { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            LeadOutputModel lom = (LeadOutputModel)obj;
            if (Id == lom.Id &&
                FirstName == lom.FirstName &&
                LastName == lom.LastName &&
                Patronymic == lom.Patronymic &&
                Login == lom.Login &&
                Phone == lom.Phone &&
                Email == lom.Email &&
                Address == lom.Address &&
                BirthDate == lom.BirthDate &&
                RegistrationDate == lom.RegistrationDate &&
                ChangeDate == lom.ChangeDate &&
                Role == lom.Role &&
                City == lom.City &&
                IsDeleted == lom.IsDeleted)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
