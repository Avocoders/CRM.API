using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.DTO
{
    public class LeadDTO
    {
        public LeadDTO(Int64 id, byte roleId, string firstName, string lastName, string patronymic, DateTime birthDate, string login, string password, string email, string phone, int cityId, string address, DateTime registrationDate, DateTime changeDate)
        {
            Id = id;
            RoleId = roleId;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            BirthDate = birthDate;
            Login = login;
            Password = password;
            Email = email;
            Phone = phone;
            CityId = cityId;
            Address = address;
            RegistrationDate = registrationDate;
            ChangeDate = changeDate;
        }


        public Int64 Id { get; set; }
        public byte RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ChangeDate { get; set; }          
    }
}
