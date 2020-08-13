using CRM.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.NUnitTest
{
    public class AccountOuputModelMock
    {
        public LeadWithAccountsOutputModel GetAccountOfLeadMock(int num)
        {
            switch (num)
            {
                case 1:
                    return new LeadWithAccountsOutputModel()
                    {
                        Id = 1,
                        FirstName = "Alena",
                        LastName = "Nuratova",
                        BirthDate = "01.01.1970",
                        Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                    {
                                        Id = 1,
                                        IsDeleted = false,
                                        CurrencyCode = "RUB",
                                        CurrencyName = "RussianRuble"
                                    },
                        },
                        IsDeleted = false

                    }; break;
                case 3:
                    return new LeadWithAccountsOutputModel()
                    {
                        Id = 4,
                        FirstName = "Elena",
                        LastName = "Galich",
                        BirthDate = "11.04.1980",
                        Accounts = new List<AccountOutputModel>()
                            {new AccountOutputModel()
                        {
                            Id = 3,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                        new AccountOutputModel()
                        {
                            Id = 12,
                            IsDeleted = false,
                            CurrencyCode = "JPY",
                            CurrencyName = "Yen"
                        },
                    },
                        IsDeleted = false

                    }; break;
                case 4:
                    return new LeadWithAccountsOutputModel()
                    {
                        Id = 4,
                        FirstName = "Ivan",
                        LastName = "Piratov",
                        BirthDate = "01.12.1997",
                        Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                    {
                                           Id = 4,
                                           IsDeleted = false,
                                           CurrencyCode = "JPY",
                                           CurrencyName = "Yen"
                                    },
                        },
                        IsDeleted = false

                    }; break;
                case 78:
                    return new LeadWithAccountsOutputModel();

            }
            return new LeadWithAccountsOutputModel();
        }
    }
}
    