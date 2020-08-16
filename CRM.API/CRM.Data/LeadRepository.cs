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

        public LeadRepository(IOptions<DatabaseOptions> options)
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


        public DataWrapper <List<AccountDto>> GetAccountByLeadId(long leadId)  
        {
            var result = new DataWrapper <List<AccountDto>>();
            try
            {               
                    result.Data = _connection.Query<AccountDto>(
                    "Account_GetByLeadId",
                    new { leadId },
                    commandType: CommandType.StoredProcedure).ToList();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
           
        }
                                            
        public DataWrapper<LeadDto> AddOrUpdateLead(LeadDto leadDto)
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var result = new DataWrapper<LeadDto>();
            try
            {
                result.Data = _connection.Query<LeadDto, RoleDto, CityDto, AccountDto, LeadDto>(
                    "Lead_Add_Or_Update",
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

        public DataWrapper<int> CheckEmail(string email)
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

        public DataWrapper<byte> GetCurrencyByAccountId(long accountId)
        {
            var result = new DataWrapper<byte>();
            try
            {
                string sqlExpression = "GetCurrencyByAccountId";
                var currency = _connection.Query<byte>(sqlExpression, new { accountId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.Data = currency;
                result.IsOk = true;
            }

            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }
    }
}
