﻿using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Data;
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
                Role = searchParameters.Role,
                FirstName = searchParameters.FirstName,
                LastName = searchParameters.LastName,
                Patronymic = searchParameters.Patronymic,
                Login = searchParameters.Login,
                Phone = searchParameters.Phone,
                Email = searchParameters.Email,
                City = searchParameters.City,
                Address = searchParameters.Address,
                BirthDate = searchParameters.BirthDate,
                //BirthDate = Convert.ToDateTime(searchParameters.BirthDate),
                RegistrationDate = searchParameters.RegistrationDate,
                //RegistrationDate = Convert.ToDateTime(searchParameters.RegistrationDate),
                IncludeDeleted = searchParameters.IsDeleted
            };
        }

        public LeadOutputModel ConvertLeadDtoToLeadOutputModel(LeadDto leadModel)
        {
            return new LeadOutputModel()
            {
                Id= (long)leadModel.Id,
                Role = leadModel.Role.Name,  
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,                
                Login = leadModel.Login,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                City = leadModel.City.Name, 
                Address = leadModel.Address,
                BirthDate = leadModel.BirthDate.ToString("dd.MM.yyyy"),
                RegistrationDate = leadModel.RegistrationDate.ToString("dd.MM.yyyy HH:mm:ss"),
                ChangeDate = leadModel.ChangeDate.ToString("dd.MM.yyyy HH:mm:ss")
            };
        }

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

        public LeadOutputModel ConvertDtoToLeadOutputModel(LeadSearchParameters leadModel)
        {
            return new LeadOutputModel()
            {
                Id = (long)leadModel.Id,
                Role = leadModel.Role,
                FirstName = leadModel.FirstName,
                LastName = leadModel.LastName,
                Patronymic = leadModel.Patronymic,
                Login = leadModel.Login,
                Phone = leadModel.Phone,
                Email = leadModel.Email,
                City = leadModel.City,
                Address = leadModel.Address,
                BirthDate = leadModel.BirthDate,
                RegistrationDate = leadModel.RegistrationDate,
                ChangeDate = leadModel.ChangeDate.ToString("dd.MM.yyyy HH:mm:ss")
            };
        }

        public List<LeadOutputModel> ConvertDtosToLeadOutputModels(List<LeadSearchParameters> leadModels)
        {
            List<LeadOutputModel> leads = new List<LeadOutputModel>();
            foreach (var lead in leadModels)
            {
                if (lead != null)
                {
                    leads.Add(new Mapper().ConvertDtoToLeadOutputModel(lead));
                }
            }
            return leads;
        }
    }
}
