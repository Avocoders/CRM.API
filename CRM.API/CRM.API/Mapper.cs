using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Data;
using CRM.Data.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;

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

        public LeadSearchParameters ConvertSearchParametersInputModelToLeadSearchParameters(SearchParametersInputModel InputSearchParams)
        {
            return new LeadSearchParameters()
            {
                RoleId = InputSearchParams.RoleId,
                FirstName = InputSearchParams.FirstName,
                LastName = InputSearchParams.LastName,
                Patronymic = InputSearchParams.Patronymic,
                Login = InputSearchParams.Login,
                Phone = InputSearchParams.Phone,
                Email = InputSearchParams.Email,
                CityId = InputSearchParams.CityId,
                Address = InputSearchParams.Address,                
                BirthDate = string.IsNullOrEmpty(InputSearchParams.BirthDate) ? null : (DateTime?)DateTime.ParseExact(InputSearchParams.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),                
                RegistrationDate = string.IsNullOrEmpty(InputSearchParams.RegistrationDate) ? null : (DateTime?)DateTime.ParseExact(InputSearchParams.RegistrationDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                IncludeDeleted = InputSearchParams.IncludeDeleted
            };
        }

        public LeadOutputModel ConvertLeadDtoToLeadOutputModel(LeadDto leadDto) => new LeadOutputModel()
        {
            Id = (long)leadDto.Id,
            Role = leadDto.Role.Name,
            FirstName = leadDto.FirstName,
            LastName = leadDto.LastName,
            Patronymic = leadDto.Patronymic,
            Login = leadDto.Login,
            Phone = leadDto.Phone,
            Email = leadDto.Email,
            City = leadDto.City.Name,
            Address = leadDto.Address,
            BirthDate = ((DateTime)leadDto.BirthDate).ToString("dd.MM.yyyy"),
            RegistrationDate = ((DateTime)leadDto.RegistrationDate).ToString("dd.MM.yyyy HH:mm:ss"),
            ChangeDate = leadDto.ChangeDate.ToString("dd.MM.yyyy HH:mm:ss")            
        };

        public List<LeadOutputModel> ConvertLeadDtosToLeadOutputModels(List<LeadDto> leadDtos)
        {
            List<LeadOutputModel> leads = new List<LeadOutputModel>();
            foreach (var lead in leadDtos)
            {
                if (lead != null)
                {
                    leads.Add(new Mapper().ConvertLeadDtoToLeadOutputModel(lead));
                }
            }
            return leads;
        }        
    }
}
