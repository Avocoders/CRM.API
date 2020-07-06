using CRM.API.Models.Input;
using CRM.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API
{
    public class Mapper
    {
        public LeadDTO ConvertLeadInputModelToLeadDTO(LeadInputModel leadModel)
        {
            return new LeadDTO()
            {                
                RoleId = leadModel.RoleId,
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,
                Login = leadModel.Login,
                Password = leadModel.Password,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                CityId = leadModel.CityId,
                Address = leadModel.Address,
                BirthDate = Convert.ToDateTime(leadModel.BirthDate),
                RegistrationDate = Convert.ToDateTime(leadModel.RegistrationDate),
                ChangeDate = Convert.ToDateTime(leadModel.ChangeDate),
            };
        }


        public CityDTO ConvertCityInputModelToCityDTO(CityInputModel cityModel)
        {
            return new CityDTO()
            {
                Name = cityModel.Name,                
            };
        }
    }
}
