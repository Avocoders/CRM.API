using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Data.DTO;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API
{
    public class Mapper
    {
        public LeadDto ConvertLeadInputModelToLeadDTO(LeadInputModel leadModel)
        {
            return new LeadDto()
            {   
                Id = leadModel.Id,                
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,
                Login = leadModel.Login,
                Password = leadModel.Password,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                City = new CityDto()
                {
                    Id = leadModel.CityId
                },
                Role = new RoleDto(),
                Address = leadModel.Address,
                BirthDate = Convert.ToDateTime(leadModel.BirthDate),                
            };
        }

        public LeadOutputModel ConvertLeadDTOToLeadOutputModel(LeadDto leadModel)
        {
            return new LeadOutputModel()
            {
                Id= (long)leadModel.Id,
                Role = leadModel.Role.Name,  //пока закомментила
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,                
                Login = leadModel.Login,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                City = leadModel.City.Name,  //пока закомментила
                Address = leadModel.Address,
                BirthDate = Convert.ToDateTime(leadModel.BirthDate),
                RegistrationDate = Convert.ToDateTime(leadModel.RegistrationDate),
                ChangeDate = Convert.ToDateTime(leadModel.ChangeDate),
            };
        }

        public List<LeadOutputModel> ConvertLeadDtosToLeadOutputModels(List<LeadDto> leadModels)
        {
            List<LeadOutputModel> leads = new List<LeadOutputModel>();
            foreach (var lead in leadModels)
            {
                if (lead != null)
                {
                    leads.Add(new Mapper().ConvertLeadDTOToLeadOutputModel(lead)
                        );
                }
            }
            return leads;
        }
    }
}
