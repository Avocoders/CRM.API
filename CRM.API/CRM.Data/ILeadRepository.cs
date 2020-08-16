﻿using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.Data
{
    public interface ILeadRepository
    {
        DataWrapper<LeadDto> GetAccountById(long Id);
        DataWrapper<LeadDto> AddOrUpdateLead(LeadDto leadDto);
        void Delete(long id);
        DataWrapper<int> CheckEmail(string email);
        DataWrapper<int> FindLeadByLogin(string login);
        DataWrapper<LeadDto> GetById(long leadId);
        DataWrapper<AuthorizationDto> GetByLogin(string login);
        DataWrapper<List<LeadDto>> SearchLeads(LeadSearchParameters searchParameters);
        DataWrapper<string> UpdateEmailByLeadId(long? id, string email);    
        DataWrapper<LeadDto> AddOrUpdateAccount(AccountDto accountDto);
        DataWrapper<List<AccountDto>> GetAccountByLeadId(long leadId);
        DataWrapper<byte> GetCurrencyByAccountId(long accountId);

    }
}