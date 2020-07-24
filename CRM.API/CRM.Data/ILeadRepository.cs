using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.Data
{
    public interface ILeadRepository
    {
        DataWrapper<LeadDto> Add(LeadDto leadDto);
        void Delete(long id);
        DataWrapper<int> FindLeadByEmail(string email);
        DataWrapper<int> FindLeadByLogin(string login);
        DataWrapper<List<LeadDto>> GetAll();
        DataWrapper<LeadDto> GetById(long leadId);
        DataWrapper<LeadDto> GetByLogin(string login);
        DataWrapper<List<LeadDto>> SearchLeads(LeadSearchParameters searchParameters);
        DataWrapper<LeadDto> Update(LeadDto leadDto);
        DataWrapper<string> UpdateEmailByLeadId(long? id, string email);
    }
}