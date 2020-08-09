﻿using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.Data
{
    public interface ILeadRepository
    {
        DataWrapper<LeadDto> GetAccountById(long Id);
        DataWrapper<LeadDto> Add(LeadDto leadDto);
        void Delete(long id);
        DataWrapper<int> FindLeadByEmail(string email);
        DataWrapper<int> FindLeadByLogin(string login);
        DataWrapper<List<LeadDto>> GetAll();
        DataWrapper<LeadDto> GetById(long leadId);
        DataWrapper<AuthorizationDto> GetByLogin(string login);
        DataWrapper<List<LeadDto>> SearchLeads(LeadSearchParameters searchParameters);
        DataWrapper<LeadDto> Update(LeadDto leadDto);
        DataWrapper<string> UpdateEmailByLeadId(long? id, string email);
        
        DataWrapper<LeadDto> AddOrUpdateAccount(AccountDto accountDto);
        DataWrapper<LeadDto> GetAccountByLeadId(long leadId);
        DataWrapper<byte> GetCurrencyByAccountId(long accountId);

    }
}