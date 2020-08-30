using CRM.Core;
using CRM.Data.DTO;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class LeadRepository : ILeadRepository
    {
        private readonly IDbConnection _connection;

        public LeadRepository(IOptions<DatabaseOptions> options)
        {
            _connection = new SqlConnection(options.Value.DBConnectionString);
        }

        public async ValueTask<DataWrapper<AccountDto>> GetAccountById(long Id)
        {
            var result = new DataWrapper<AccountDto>();
            try
            {
                var tmp = await _connection.QueryAsync<AccountDto, LeadDto, CityDto, AccountDto>(
                      StoredProcedures.AccountGetById,
                      (account, lead, city) =>
                      {
                          AccountDto accoutEntry;
                          accoutEntry = account;
                          accoutEntry.Lead = lead;
                          accoutEntry.Lead.City = city;
                          return accoutEntry;
                      },
                      new { Id }, splitOn: "Id",
                      commandType: CommandType.StoredProcedure);
                result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<List<AccountDto>>> GetAccountsByLeadId(long leadId)
        {
            var result = new DataWrapper<List<AccountDto>>();
            try
            {
                var tmp = await _connection.QueryAsync<AccountDto>(
                StoredProcedures.AccountGetByLeadId,
                new { leadId },
                commandType: CommandType.StoredProcedure);
                result.Data = tmp.ToList();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;

        }

        public async ValueTask<DataWrapper<LeadDto>> AddOrUpdateLead(LeadDto leadDto)
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var result = new DataWrapper<LeadDto>();
            try
            {
                var tmp = await _connection.QueryAsync<LeadDto, RoleDto, CityDto, AccountDto, LeadDto>(
                    StoredProcedures.LeadAddOrUpdate,
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
                    commandType: CommandType.StoredProcedure);
               result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<AccountDto>> AddOrUpdateAccount(AccountDto accountDto)
        {
            var result = new DataWrapper<AccountDto>();
            try
            {
                var tmp = await _connection.QueryAsync<AccountDto, LeadDto, CityDto, AccountDto>(
                    StoredProcedures.AccountAddOrUpdate,
                    (account, lead, city) =>
                    {
                        AccountDto accoutEntry;
                        accoutEntry = account;
                        accoutEntry.Lead = lead;
                        accoutEntry.Lead.City = city;
                        return accoutEntry;
                    },
                    new
                    {
                        accountDto.Id,
                        accountDto.LeadId,
                        accountDto.CurrencyId
                    }, splitOn: "Id", commandType: CommandType.StoredProcedure);
                  result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask Delete(long id)
        {
           var tmp = await _connection.ExecuteAsync(StoredProcedures.LeadDelete, new { id }, commandType: CommandType.StoredProcedure);
        }

        public async ValueTask<DataWrapper<LeadDto>> GetById(long leadId)
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var result = new DataWrapper<LeadDto>();
            try
            {
                var tmp = await _connection.QueryAsync<LeadDto, RoleDto, CityDto, AccountDto, LeadDto>(
                    StoredProcedures.LeadGetById,
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
                    commandType: CommandType.StoredProcedure);
               result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<AuthorizationDto>> GetByLogin(string login)
        {
            var result = new DataWrapper<AuthorizationDto>();
            try
            {
                var tmp = await _connection.QueryAsync<AuthorizationDto, RoleDto, AuthorizationDto>(
                    StoredProcedures.LeadGetByLogin,
                    (lead, role) =>
                    {
                        AuthorizationDto leadEntry;

                        leadEntry = lead;
                        leadEntry.Role = role;
                        return leadEntry;
                    },
                    new { login },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure);
                result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }
       
        public async ValueTask<DataWrapper<int>> FindLeadByLogin(string login)
        {
            var result = new DataWrapper<int>();
            try
            {
                var tmp = await _connection.QueryAsync<int>(StoredProcedures.LeadFindByLogin, new { login }, commandType: CommandType.StoredProcedure);
               result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<int>> CheckEmail(string email)
        {
            var result = new DataWrapper<int>();
            try
            {
                var tmp = await _connection.QueryAsync<int>(StoredProcedures.LeadFindByEmail, new { email }, commandType: CommandType.StoredProcedure);
               result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<string>> UpdateEmailByLeadId(EmailDto emailDto)
        {
            var result = new DataWrapper<string>();
            try
            {
                var tmp = await _connection.QueryAsync<string>(StoredProcedures.LeadUpdateEmail, new { emailDto.LeadId, emailDto.Email }, commandType: CommandType.StoredProcedure);
                result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<List<LeadDto>>> SearchLeads(LeadSearchParameters searchParameters)
        {
            var leadDictionary = new Dictionary<long, LeadDto>();
            var results = new DataWrapper<List<LeadDto>>();
            LeadDto leadEntry = new LeadDto();
            try
            {
                var tmp = await _connection.QueryAsync<LeadDto, RoleDto, CityDto, AccountDto, LeadDto>(
                    StoredProcedures.LeadSearch,
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
                    commandType: CommandType.StoredProcedure);
               results.Data = tmp.ToList();
                results.IsOk = true;
                results.Data = new List<LeadDto> (leadDictionary.Values);
            }
            catch (Exception e)
            {
                results.ExceptionMessage = e.Message;
            }
            return results;
        }

        public async ValueTask<DataWrapper<byte>> GetCurrencyByAccountId(long accountId)
        {
            var result = new DataWrapper<byte>();
            try
            {
                string sqlExpression = StoredProcedures.GetCurrencyByAccountId;
                var tmp = await _connection.QueryAsync<byte>(sqlExpression, new { accountId }, commandType: CommandType.StoredProcedure);
                var currency = tmp.FirstOrDefault();
                result.Data = currency;
                result.IsOk = true;
            }

            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask UpdatePassword(PasswordDto passwordDto)
        {
           var tmp = await _connection.ExecuteAsync("UpdatePassword", new { passwordDto.Id, passwordDto.Password },commandType: CommandType.StoredProcedure);
        }

        public async ValueTask<DataWrapper<int>> AccountFindById(long accountId)
        {
            var result = new DataWrapper<int>();
            try
            {
                var tmp = await _connection.QueryAsync<int>(StoredProcedures.AccountFindById, new { accountId }, commandType: CommandType.StoredProcedure);
                result.Data = tmp.FirstOrDefault();
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
