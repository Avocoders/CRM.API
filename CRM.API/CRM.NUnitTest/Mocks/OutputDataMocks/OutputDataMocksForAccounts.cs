using CRM.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.NUnitTest
{
    public class OutputDataMocksForAccounts
    {
        public dynamic GetAccountOutputModelMockById(int num)
        {
            return num switch
            {
                1 => new LeadWithAccountsOutputModel()
                {
                    Id = 10,
                    FirstName = "Oksi",
                    LastName = "Miron",
                    BirthDate = "12.04.1965",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 18,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                2 => new LeadWithAccountsOutputModel()
                {
                    Id = 2,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    BirthDate = "01.08.1995",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 19,
                            IsDeleted = false,
                            CurrencyCode = "JPY",
                            CurrencyName = "Yen"
                        },
                    },
                    IsDeleted = false
                },
                3 => new LeadWithAccountsOutputModel()
                {
                    Id = 5,
                    FirstName = "Sergei",
                    LastName = "Piratov",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 20,
                            IsDeleted = false,
                            CurrencyCode = "RUB",
                            CurrencyName = "RussianRuble"
                        },
                    },
                    IsDeleted = false
                },
                4 => new LeadWithAccountsOutputModel()
                {
                    Id = 1,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    BirthDate = "01.01.1970",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 21,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false
                },
                5 => new LeadWithAccountsOutputModel()
                {
                    Id = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 22,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                6 => new LeadWithAccountsOutputModel()
                {
                    Id = 4,
                    FirstName = "Ivan",
                    LastName = "Piratov",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 23,
                            IsDeleted = false,
                            CurrencyCode = "RUB",
                            CurrencyName = "RussianRubble"
                        },
                    },
                    IsDeleted = false
                },
                _ => new LeadWithAccountsOutputModel(),
            };
        }

        public dynamic GetAccountOfLeadMock(int num)
        {
            return num switch
            {
                1 => new LeadWithAccountsOutputModel()
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
                },
                3 => new LeadWithAccountsOutputModel()
                {
                    Id = 3,
                    FirstName = "Elena",
                    LastName = "Galich",
                    BirthDate = "11.04.1980",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 3,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false

                },
                4 => new LeadWithAccountsOutputModel()
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
                },
                78 => null,
                _ => new LeadWithAccountsOutputModel(),
            };
        }

        public LeadWithAccountsOutputModel GetAccountByLeadOfLeadMock(int num)
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
                        Id = 3,
                        FirstName = "Elena",
                        LastName = "Galich",
                        BirthDate = "11.04.1980",
                        Accounts = new List<AccountOutputModel>()
                            {
                            new AccountOutputModel()
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
                    return null;

            }
            return new LeadWithAccountsOutputModel();
        }

        

        public LeadWithAccountsOutputModel UpdateAccountByLeadOfLeadMock(int num)
        {
            switch (num)
            {
                case 1:
                    return new LeadWithAccountsOutputModel()
                    {
                        Id = 8,
                        FirstName = "Oksana",
                        LastName = "Galich",
                        BirthDate = "11.04.1965",
                        Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                    {
                                        Id = 8,
                                        IsDeleted = false,
                                        CurrencyCode = "USD",
                                        CurrencyName = "USDollar"
                                    },
                        },
                        IsDeleted = false

                    }; break;

                case 2:
                    return new LeadWithAccountsOutputModel()
                    {
                        Id = 2,
                        FirstName = "Pavel",
                        LastName = "Muratov",
                        BirthDate = "01.08.1995",
                        Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                    {
                                        Id = 11,
                                        IsDeleted = false,
                                        CurrencyCode = "RUB",
                                        CurrencyName = "RussianRuble"
                                    },
                        },
                        IsDeleted = false

                    }; break;
                case 3:
                    return null;
                case 4:
                    return new LeadWithAccountsOutputModel()
                    {
                        Id = 9,
                        FirstName = "Vlada",
                        LastName = "Gala",
                        BirthDate = "11.04.1969",
                        Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                    {
                                        Id = 9,
                                        IsDeleted = false,
                                        CurrencyCode = "EUR",
                                        CurrencyName = "Euro"
                                    },
                        },
                        IsDeleted = false

                    }; break;
            }
            return new LeadWithAccountsOutputModel();
        }
    }
}
