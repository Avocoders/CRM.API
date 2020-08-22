using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.Data
{
    public interface ILeadRepository
    {
        void UpdatePassword(PasswordDto passwordDto);
        DataWrapper<AccountDto> GetAccountById(long Id);
        DataWrapper<LeadDto> AddOrUpdateLead(LeadDto leadDto);
        void Delete(long id);
        DataWrapper<int> CheckEmail(string email);
        DataWrapper<int> FindLeadByLogin(string login);
        DataWrapper<LeadDto> GetById(long leadId);
        DataWrapper<AuthorizationDto> GetByLogin(string login);
        DataWrapper<List<LeadDto>> SearchLeads(LeadSearchParameters searchParameters);
        DataWrapper<string> UpdateEmailByLeadId(EmailDto emailDto);
        DataWrapper<AccountDto> AddOrUpdateAccount(AccountDto accountDto);
        DataWrapper<List<AccountDto>> GetAccountsByLeadId(long leadId);
        DataWrapper<byte> GetCurrencyByAccountId(long accountId);
    }
}