using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.Data
{
    public interface ILeadRepository
    {
        DataWrapper<AccountWithLeadDto> GetAccountById(long Id);
        void UpdatePassword(PasswordDto passwordDto);
        DataWrapper<LeadDto> AddOrUpdateLead(LeadDto leadDto);
        void Delete(long id);
        DataWrapper<int> CheckEmail(string email);
        DataWrapper<int> FindLeadByLogin(string login);
        DataWrapper<LeadDto> GetById(long leadId);
        DataWrapper<AuthorizationDto> GetByLogin(string login);
        DataWrapper<List<LeadDto>> SearchLeads(LeadSearchParameters searchParameters);
        DataWrapper<string> UpdateEmailByLeadId(EmailDto emailDto);
        DataWrapper<AccountWithLeadDto> AddOrUpdateAccount(AccountDto accountDto);
        DataWrapper<List<AccountDto>> GetAccountByLeadId(long leadId);
        DataWrapper<byte> GetCurrencyByAccountId(long accountId);
    }
}