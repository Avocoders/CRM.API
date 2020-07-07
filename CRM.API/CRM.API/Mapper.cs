using CRM.API.Models.Input;
using CRM.API.Models.Output;
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
                Id=leadModel.Id,
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

        public LeadOutputModel ConvertLeadOutputModelToLeadDTO(LeadDTO leadModel)
        {
            return new LeadOutputModel()
            {
                Id=leadModel.Id,
                Role = leadModel.Role,
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,
                Password=leadModel.Password,
                Login = leadModel.Login,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                City = leadModel.City,
                Address = leadModel.Address,
                BirthDate = Convert.ToDateTime(leadModel.BirthDate),
                RegistrationDate = Convert.ToDateTime(leadModel.RegistrationDate),
                ChangeDate = Convert.ToDateTime(leadModel.ChangeDate),
            };
        }

        public List<LeadOutputModel> ConvertListLeadOutputModelToListLeadDTO(List<LeadDTO> leadModel)
        {
            List<LeadOutputModel> leads = new List<LeadOutputModel>();
            foreach (var lead in leadModel)
            {
                if (lead != null)
                {
                    leads.Add(
                        new LeadOutputModel()
                        {
                            Id = lead.Id,
                            Role = lead.Role,
                            FirstName = lead.FirstName,
                            LastName = lead.LastName,
                            Patronymic = lead.Patronymic,
                            Login = lead.Login,
                            Phone = lead.Phone,
                            Email = lead.Email,
                            City = lead.City,
                            Address = lead.Address,
                            BirthDate = Convert.ToDateTime(lead.BirthDate),
                            RegistrationDate = Convert.ToDateTime(lead.RegistrationDate),
                            ChangeDate = Convert.ToDateTime(lead.ChangeDate),
                        }
                        );
                }
            }
            return leads;
        }
    }
}
