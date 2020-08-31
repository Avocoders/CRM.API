using CRM.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Data
{
    public interface ILeadRepository
    {
        ValueTask UpdatePassword(PasswordDto passwordDto);
        ValueTask<DataWrapper<LeadDto>> AddOrUpdateLead(LeadDto leadDto);
        ValueTask Delete(long id);
        ValueTask<DataWrapper<int>> CheckEmail(string email);
        ValueTask<DataWrapper<int>> FindLeadByLogin(string login);
        ValueTask<DataWrapper<LeadDto>> GetById(long leadId);
        ValueTask<DataWrapper<AuthorizationDto>> GetByLogin(string login);
        ValueTask<DataWrapper<List<LeadDto>>> SearchLeads(LeadSearchParameters searchParameters);
        ValueTask<DataWrapper<string>> UpdateEmailByLeadId(EmailDto emailDto);
        ValueTask<DataWrapper<AccountDto>> AddOrUpdateAccount(AccountDto accountDto);
        ValueTask<DataWrapper<byte>> GetCurrencyByAccountId(long accountId);
        ValueTask<DataWrapper<int>> AccountFindById(long accountId);        ValueTask<DataWrapper<AccountDto>> GetAccountById(long Id);        ValueTask<DataWrapper<List<AccountDto>>> GetAccountsByLeadId(long leadId);
    }
}