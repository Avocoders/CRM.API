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

        public LeadSearchParameters ConvertSearchParametersInputModelToLeadDTO(SearchParametersInputModel searchParameters)
        {
            return new LeadSearchParameters()
            {
                RoleId = searchParameters.RoleId,
                FirstName = searchParameters.FirstName,
                LastName = searchParameters.LastName,
                Patronymic = searchParameters.Patronymic,
                Login = searchParameters.Login,
                Phone = searchParameters.Phone,
                Email = searchParameters.Email,
                CityId = searchParameters.CityId,
                Address = searchParameters.Address,                
                BirthDate = string.IsNullOrEmpty(searchParameters.BirthDate) ? null : (DateTime?)DateTime.ParseExact(searchParameters.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),                
                RegistrationDate = string.IsNullOrEmpty(searchParameters.RegistrationDate) ? null : (DateTime?)DateTime.ParseExact(searchParameters.RegistrationDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                IncludeDeleted = searchParameters.IncludeDeleted
            };
        }

        public LeadOutputModel ConvertLeadDtoToLeadOutputModel(LeadDto leadModel) => new LeadOutputModel()
        {
            Id = (long)leadModel.Id,
            Role = leadModel.Role.Name,
            FirstName = leadModel.FirstName,
            LastName = leadModel.LastName,
            Patronymic = leadModel.Patronymic,
            Login = leadModel.Login,
            Phone = leadModel.Phone,
            Email = leadModel.Email,
            City = leadModel.City.Name,
            Address = leadModel.Address,
            BirthDate = ((DateTime)leadModel.BirthDate).ToString("dd.MM.yyyy"),
            RegistrationDate = ((TimeSpan)leadModel.RegistrationDate).ToString("dd.MM.yyyy HH:mm:ss"),
            ChangeDate = leadModel.ChangeDate.ToString("dd.MM.yyyy HH:mm:ss")
            //string sqlFormattedDate = ((DateTime)myDate).ToString("yyyy-MM-dd HH:mm:ss")
        };

        public List<LeadOutputModel> ConvertLeadDtosToLeadOutputModels(List<LeadDto> leadModels)
        {
            List<LeadOutputModel> leads = new List<LeadOutputModel>();
            foreach (var lead in leadModels)
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
