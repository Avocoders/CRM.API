using CRM.Core;
using CRM.Data.DTO;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;  

namespace CRM.Data
{
    public class LeadRepository : ILeadRepository
    {
        private readonly IDbConnection _connection;

        public LeadRepository(IOptions<StorageOptions> options)
        {
            _connection = new SqlConnection(options.Value.DBConnectionString);
        }

        public LeadRepository()
        { }

        public DataWrapper<LeadDto> GetAccountById(long Id)  
        { 
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, AccountDto, LeadDto>(
                    "Account_GetById",
                    (lead,account) =>
                    {
                        LeadDto leadEntry;
                        leadEntry = lead;
                        leadEntry.Accounts = new List<AccountDto>();
                        leadEntry.Accounts.Add(account);
                        return leadEntry;
                    },
                    new { Id }, splitOn: "Id",
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }


        public DataWrapper <LeadDto> GetAccountByLeadId(long leadId)  
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var result = new DataWrapper <LeadDto>();
            try
            {               
                    result.Data = _connection.Query<LeadDto, AccountDto, LeadDto>(
                    "Account_GetByLeadId",
                    (lead, account) =>
                    {
                    LeadDto leadEntry;
                    if (!leadDictionary.TryGetValue(lead.Id.Value, out leadEntry))
                        {
                            leadEntry = lead;
                            leadEntry.Accounts = new List<AccountDto>();
                            leadDictionary.Add(leadEntry.Id.Value, leadEntry);
                        }
                        leadEntry.Accounts.Add(account);
                        return leadEntry;
  
                    },
                    new { leadId }, splitOn: "Id",
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
           
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
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<LeadDto> AddOrUpdateAccount(AccountDto accountDto)
        {
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto,AccountDto,LeadDto>( 
                    "Account_Add_Or_Update ",
                     (lead, account) =>
                     {
                         LeadDto leadEntry;
                         leadEntry = lead;
                         leadEntry.Accounts = new List<AccountDto>();
                         leadEntry.Accounts.Add(account);
                         return leadEntry;
                     },

                       new
                       {
                           accountDto.Id,
                           accountDto.LeadId,
                           accountDto.CurrencyId
                       }, splitOn: "Id", commandType: CommandType.StoredProcedure).FirstOrDefault();
            
                result.IsOk = true;
            }
            catch (Exception e)
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
                results.Data = _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
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
            catch (Exception e)
            {
                results.ExceptionMessage = e.Message;
            }
            return results;
        }

        public DataWrapper<LeadDto> GetById(long leadId)
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, RoleDto, CityDto, AccountDto, LeadDto>(
                    "Lead_GetById",
                    (lead, role, city, account) =>
                    {
                        LeadDto leadEntry;
                        if (!leadDictionary.TryGetValue(lead.Id.Value, out leadEntry))
                        {
                            leadEntry = lead;
                            leadEntry.Accounts = new List<AccountDto>();
                            leadDictionary.Add(leadEntry.Id.Value, leadEntry);
                            leadEntry.Role = role;
                            leadEntry.City = city;
                            
                        }
                        leadEntry.Accounts.Add(account);
                        return leadEntry;
                    },
                    new { leadId },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;


                }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<AuthorizationDto> GetByLogin(string login)
        {
            var result = new DataWrapper<AuthorizationDto>();
            try
            {
                result.Data = _connection.Query<AuthorizationDto, RoleDto, AuthorizationDto>(
                    "Lead_GetByLogin",
                    (lead, role) =>
                    {
                        AuthorizationDto leadEntry;

                        leadEntry = lead;
                        leadEntry.Role = role;
                        return leadEntry;
                    },
                    new { login },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
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
                result.IsOk = true;
            }
            catch (Exception e)
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
                result.IsOk = true;
            }
            catch (Exception e)
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
                result.IsOk = true;
            }
            catch (Exception e)
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
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<List<LeadDto>> SearchLeads(LeadSearchParameters searchParameters)
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var results = new DataWrapper<List<LeadDto>>();
            try
            {
                results.Data = _connection.Query<LeadDto, RoleDto, CityDto, AccountDto, LeadDto>(
                    "Lead_Search",
                    (lead, role, city, account) =>
                    {
                        LeadDto leadEntry;
                        if (!leadDictionary.TryGetValue(lead.Id.Value, out leadEntry))
                        {
                            leadEntry = lead;
                            leadEntry.Accounts = new List<AccountDto>();
                            leadDictionary.Add(leadEntry.Id.Value, leadEntry);
                            leadEntry.Role = role;
                            leadEntry.City = city;

                        }
                        leadEntry.Accounts.Add(account);
                        return leadEntry;
                    },
                    searchParameters, splitOn: "Id",
                    commandType: CommandType.StoredProcedure).ToList();
                results.IsOk = true;
            }
            catch (Exception e)
            {
                results.ExceptionMessage = e.Message;
            }
            return results;
        }
    }
}
