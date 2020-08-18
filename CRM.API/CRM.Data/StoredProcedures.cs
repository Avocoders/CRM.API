using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data
{
    public static class StoredProcedures
    {
        public const string AccountGetById = "Account_GetById";
        public const string AccountGetByLeadId = "Account_GetByLeadId";
        public const string LeadAddOrUpdate = "Lead_Add_Or_Update";
        public const string UpdatePassword = "UpdatePassword";
        public const string AccountAddOrUpdate = "Account_Add_Or_Update";
        public const string LeadDelete = "Lead_Delete";
        public const string LeadGetById = "Lead_GetById";
        public const string LeadGetByLogin = "Lead_GetByLogin";
        public const string LeadFindByLogin = "Lead_FindByLogin";
        public const string LeadFindByEmail = "Lead_FindByEmail";
        public const string LeadUpdateEmail = "Lead_UpdateEmail";
        public const string LeadSearch = "Lead_Search";
        public const string GetCurrencyByAccountId = "GetCurrencyByAccountId";
    }
}
