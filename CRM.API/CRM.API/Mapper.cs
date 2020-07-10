using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Data.DTO;
using System;
using System.Collections.Generic;

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
                CityId = leadModel.CityId,
                Address = leadModel.Address,
                BirthDate = Convert.ToDateTime(leadModel.BirthDate),                
            };
        }

        public LeadOutputModel ConvertLeadOutputModelToLeadDTO(LeadDto leadModel)
        {
            return new LeadOutputModel()
            {
                Id= (long)leadModel.Id,
                //Role = leadModel.Role,  пока закомментила
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,                
                Login = leadModel.Login,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                //City = leadModel.City,  пока закомментила
                Address = leadModel.Address,
                BirthDate = Convert.ToDateTime(leadModel.BirthDate),
                RegistrationDate = Convert.ToDateTime(leadModel.RegistrationDate),
                ChangeDate = Convert.ToDateTime(leadModel.ChangeDate),
            };
        }

        public List<LeadOutputModel> ConvertListLeadOutputModelToListLeadDTO(List<LeadDto> leadModel)
        {
            List<LeadOutputModel> leads = new List<LeadOutputModel>();
            foreach (var lead in leadModel)
            {
                if (lead != null)
                {
                    leads.Add(
                        new LeadOutputModel()
                        {
                            Id = (long)lead.Id,
                            //Role = lead.Role,
                            FirstName = lead.FirstName,
                            LastName = lead.LastName,
                            Patronymic = lead.Patronymic,
                            Login = lead.Login,
                            Phone = lead.Phone,
                            Email = lead.Email,
                            //City = lead.City,
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
