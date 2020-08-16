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
                    Id = 3,
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
                            CurrencyName = "RussiaRubble"
                        },
                    },
                    IsDeleted = false
                },
                2 => new LeadWithAccountsOutputModel()
                {
                    Id = 3,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    BirthDate = "01.01.1970",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 2,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                3 => new LeadWithAccountsOutputModel()
                {
                    Id = 3,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    BirthDate = "01.01.1970",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 2,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                4 => new LeadWithAccountsOutputModel()
                {
                    Id = 3,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    BirthDate = "01.08.1995",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 4,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                5 => new LeadWithAccountsOutputModel()
                {
                    Id = 3,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    BirthDate = "01.08.1995",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 4,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false
                },
                6 => new LeadWithAccountsOutputModel()
                {
                    Id = 11,
                    FirstName = "Elena",
                    LastName = "Galich",
                    BirthDate = "11.04.1980",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 6,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false
                },
                7 => new LeadWithAccountsOutputModel()
                {
                    Id = 3,
                    FirstName = "Elena",
                    LastName = "Galich",
                    BirthDate = "11.04.1980",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 7,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                8 => new LeadWithAccountsOutputModel()
                {
                    Id = 4,
                    FirstName = "Ivan",
                    LastName = "Piratov",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 8,
                            IsDeleted = false,
                            CurrencyCode = "RUB",
                            CurrencyName = "RussianRuble"
                        },
                    },
                    IsDeleted = false
                },
                9 => new LeadWithAccountsOutputModel()
                {
                    Id = 5,
                    FirstName = "Sergei",
                    LastName = "Piratov",
                    BirthDate = "01.12.1997",
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
                },
                10 => new LeadWithAccountsOutputModel()
                {
                    Id = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 10,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false
                },
                11 => new LeadWithAccountsOutputModel()
                {
                    Id = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 11,
                            IsDeleted = false,
                            CurrencyCode = "JPY",
                            CurrencyName = "Yen"
                        },
                    },
                    IsDeleted = false
                },
                12 => new LeadWithAccountsOutputModel()
                {
                    Id = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 12,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false
                },
                18 => new LeadWithAccountsOutputModel()
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
                19 => new LeadWithAccountsOutputModel()
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
                20 => new LeadWithAccountsOutputModel()
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
                21 => new LeadWithAccountsOutputModel()
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
                22 => new LeadWithAccountsOutputModel()
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
                23 => "Choose currency",
                _ => -1,
            };
        }       

        public List<AccountOutputModel> GetListOfAccountOutputModelsMock(int num)  
        {
            return num switch
            {
                1 => new List<AccountOutputModel>()
                {
                    new AccountOutputModel()
                    {
                        Id = 1,
                        IsDeleted = false,
                        CurrencyCode = "RUB",
                        CurrencyName = "RussianRuble"
                    },
                    new AccountOutputModel()
                    {
                        Id = 2,
                        IsDeleted = false,
                        CurrencyCode = "USD",
                        CurrencyName = "USDollar"
                    },
                    new AccountOutputModel()
                    {
                        Id = 3,
                        IsDeleted = false,
                        CurrencyCode = "USD",
                        CurrencyName = "USDollar"
                    },
                },
                3 => new List<AccountOutputModel>()
                {
                     new AccountOutputModel()
                     {
                         Id = 6,
                         IsDeleted = false,
                         CurrencyCode = "EUR",
                         CurrencyName = "Euro"
                     },
                    new AccountOutputModel()
                    {
                        Id = 7,
                        IsDeleted = false,
                        CurrencyCode = "JPY",
                        CurrencyName = "Yen"
                    },
                },
                4 => new List<AccountOutputModel>()
                {
                    new AccountOutputModel()
                    {
                        Id = 7,
                        IsDeleted = false,
                        CurrencyCode = "JPY",
                        CurrencyName = "Yen"
                    },
                },
                5 => new List<AccountOutputModel>()
                {
                    new AccountOutputModel()
                    {
                        Id = 1,
                        IsDeleted = false,
                        CurrencyCode = "RUB",
                        CurrencyName = "RussianRuble"
                    },
                },
                6 => new List<AccountOutputModel>()
                {
                    new AccountOutputModel()
                    {
                        Id = 3,
                        IsDeleted = false,
                        CurrencyCode = "USD",
                        CurrencyName = "USDollar"
                    },
                    new AccountOutputModel()
                    {
                         Id = 6,
                         IsDeleted = false,
                         CurrencyCode = "EUR",
                         CurrencyName = "Euro"
                    },
                    new AccountOutputModel()
                    {
                        Id = 7,
                        IsDeleted = false,
                        CurrencyCode = "JPY",
                        CurrencyName = "Yen"
                    },
                },
                70 => new List<AccountOutputModel>() { },
                _ => new List<AccountOutputModel>(),
            };
        }  
    }
}
