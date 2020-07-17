﻿using CRM.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRM.Data
{
    public class LeadRepository
    {
        private readonly IDbConnection _connection;

        public LeadRepository()
        {
            _connection = Connection.GetConnection();
        }

        public DataWrapper<LeadDto> Add(LeadDto leadDto)
        {
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>("Lead_Add_Or_Update",
                    (lead, role, city) =>
                    {
                        LeadDto leadEntry;

                        leadEntry = lead;
                        leadEntry.Role = role;
                        leadEntry.City = city;

                        return leadEntry;
                    },
                    new
                    {
                        leadDto.Id,
                        leadDto.FirstName,
                        leadDto.LastName,
                        leadDto.Patronymic,
                        leadDto.Login,
                        leadDto.Password,
                        leadDto.Phone,
                        leadDto.Email,
                        CityId = leadDto.City.Id,
                        leadDto.Address,
                        leadDto.BirthDate
                    }, splitOn: "Id",
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public void Delete(long id)
        {
            _connection.Execute("Lead_Delete", new { id }, commandType: CommandType.StoredProcedure);
        }

        public DataWrapper<List<LeadDto>> GetAll()
        {
            var results = new DataWrapper<List<LeadDto>>();
            try
            {
                results.Data =_connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                    "Lead_GetAll",
                    (lead, role, city) =>
                    {
                        LeadDto leadEntry;

                        leadEntry = lead;
                        leadEntry.Role = role;
                        leadEntry.City = city;
                        
                        return leadEntry;
                    },
                    splitOn: "Id").ToList();
                results.IsOk = true;
            }
            catch(Exception e)
            {
                results.ExceptionMessage = e.Message;
            }            
            return results;
        }

        public DataWrapper<LeadDto> GetById(long leadId)
        {
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                "Lead_GetById",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;

                    return leadEntry;
                },
                new { leadId },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<LeadDto> GetByLogin(string login)
        {
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                "Lead_GetByLogin",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    return leadEntry;
                },
                new { login },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;                       
        }

        public DataWrapper<LeadDto> Update(LeadDto leadDto)
        {
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>("Lead_Add_Or_Update",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    
                    return leadEntry;
                },
                new
                {
                    leadDto.Id,
                    leadDto.FirstName,
                    leadDto.LastName,
                    leadDto.Patronymic,
                    leadDto.Login,
                    leadDto.Password,
                    leadDto.Phone,
                    leadDto.Email,  
                    CityId = leadDto.City.Id,
                    leadDto.Address,
                    leadDto.BirthDate
                },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<int> FindLeadByLogin(string login)
        {
            var result = new DataWrapper<int>();
            try
            {
                result.Data = _connection.Query<int>("Lead_FindByLogin", new { login }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<int> FindLeadByEmail(string email)
        {
            var result = new DataWrapper<int>();
            try
            {
                result.Data = _connection.Query<int>("Lead_FindByEmail", new { email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<string> UpdateEmailByLeadId(long? id, string email)
        {
            var result = new DataWrapper<string>();
            try
            {
                result.Data = _connection.Query<string>("Lead_UpdateEmail", new { id, email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch(Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public List<LeadDto> SearchLeads(LeadSearchParameters searchParameters)
        {
            return _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>("Lead_Search", 
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    return leadEntry;
                },
                searchParameters, 
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).ToList();           
        }
    }
}
