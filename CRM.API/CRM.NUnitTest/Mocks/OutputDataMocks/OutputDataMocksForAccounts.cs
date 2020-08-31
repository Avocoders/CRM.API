using CRM.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.NUnitTest
{
    public class OutputDataMocksForAccounts
    {
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
                     new AccountOutputModel()
                     {
                         Id = 21,
                         IsDeleted = false,
                         CurrencyCode = "EUR",
                         CurrencyName = "Euro"
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

        public dynamic GetAccountWithLeadOutputModelMockById(int num)
        {

            return num switch
            {
                1 => new AccountWithLeadOutputModel()
                {
                    Id = 1,
                    LeadId = 1, 
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Patronymic = "Nikolaevna",
                    Phone = "+79261111111",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 5",
                    BirthDate = "01.01.1970",
                    LeadIsDeleted = false,
                    CurrencyCode = "RUB",
                    CurrencyName = "RussianRuble",
                    IsDeleted = false                   
                },
                2 => new AccountWithLeadOutputModel()
                {
                    Id = 2,
                    LeadId = 1,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Patronymic = "Nikolaevna",
                    Phone = "+79261111111",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 5",
                    BirthDate = "01.01.1970",
                    LeadIsDeleted = false,
                    CurrencyCode = "USD",
                    CurrencyName = "USDollar",
                    IsDeleted = false

                },
                3 => new AccountWithLeadOutputModel()
                {

                    Id = 3,
                    LeadId = 1,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Patronymic = "Nikolaevna",
                    Phone = "+79261111111",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 5",
                    BirthDate = "01.01.1970",
                    LeadIsDeleted = false,
                    CurrencyCode = "USD",
                    CurrencyName = "USDollar",
                    IsDeleted = false
                },
                4 => new AccountWithLeadOutputModel()
                {
                    Id = 4,
                    LeadId = 2,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    Patronymic = "Nikolaevich",
                    Phone = "+79322222222",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.08.1995",
                    LeadIsDeleted = false,
                    CurrencyCode = "USD",
                    CurrencyName = "USDollar",
                    IsDeleted = false

                },
                5 => new AccountWithLeadOutputModel()
                {

                    Id = 5,
                    LeadId = 2,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    Patronymic = "Nikolaevich",
                    Phone = "+79322222222",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.08.1995",
                    LeadIsDeleted = false,
                    CurrencyCode = "EUR",
                    CurrencyName = "Euro",
                    IsDeleted = false

                },
                6 => new AccountWithLeadOutputModel()
                {
                    Id = 6,
                    LeadId = 3,
                    FirstName = "Elena",
                    LastName = "Galich",
                    Patronymic = "Ivanovna",
                    Phone = "+79263333333",
                    City = "Saratov",
                    Address = "Stroitelei, 13, 78",
                    BirthDate = "11.04.1980",
                    LeadIsDeleted = false,
                    CurrencyCode = "EUR",
                    CurrencyName = "Euro",
                    IsDeleted = false
                },
                7 => new AccountWithLeadOutputModel()
                {                    
                    Id = 7,
                    LeadId = 3,
                    FirstName = "Elena",
                    LastName = "Galich",
                    Patronymic = "Ivanovna",
                    Phone = "+79263333333",
                    City = "Saratov",
                    Address = "Stroitelei, 13, 78",
                    BirthDate = "11.04.1980",
                    LeadIsDeleted = false,
                    CurrencyCode = "USD",
                    CurrencyName = "USDollar",
                    IsDeleted = false

                 },
                8 => new AccountWithLeadOutputModel()
                {
                    Id = 8,
                    LeadId = 4,
                    FirstName = "Ivan",
                    LastName = "Piratov",
                    Patronymic = "Nikolaevich",
                    Phone = "+79324444444",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "RUB",
                    CurrencyName = "RussianRuble",
                    IsDeleted = false

                },
                9 => new AccountWithLeadOutputModel()
                {
                    Id = 9,
                    LeadId = 5,
                    FirstName = "Sergei",
                    LastName = "Piratov",
                    Patronymic = "Nikolaevich",
                    Phone = "+79265555555",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "EUR",
                    CurrencyName = "Euro",
                    IsDeleted = false

                },
                10 => new AccountWithLeadOutputModel()
                {

                    Id = 10,
                    LeadId = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivanovna",
                    Phone = "+79267777777",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "EUR",
                    CurrencyName = "Euro",
                    IsDeleted = false

                },
                11 => new AccountWithLeadOutputModel()
                {

                    Id = 11,
                    LeadId = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivanovna",
                    Phone = "+79267777777",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "JPY",
                    CurrencyName = "Yen",
                    IsDeleted = false
                },
                12 => new AccountWithLeadOutputModel()
                {
                    Id = 12,
                    LeadId = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivanovna",
                    Phone = "+79267777777",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "EUR",
                    CurrencyName = "Euro",
                    IsDeleted = false
                },
                18 => new AccountWithLeadOutputModel()
                {
                    Id = 18,
                    LeadId = 10,
                    FirstName = "Oksi",
                    LastName = "Miron",
                    Patronymic = "Dmitrievich",
                    Phone = "+79269999955",
                    City = "Saratov",
                    Address = "Stroitelei, 13, 70",
                    BirthDate = "12.04.1965",
                    LeadIsDeleted = false,
                    CurrencyCode = "USD",
                    CurrencyName = "USDollar",
                    IsDeleted = false
                },
                19 => new AccountWithLeadOutputModel()
                {
                    Id = 19,
                    LeadId = 2,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    Patronymic = "Nikolaevich",
                    Phone = "+79322222222",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.08.1995",
                    LeadIsDeleted = false,
                    CurrencyCode = "JPY",
                    CurrencyName = "Yen",
                    IsDeleted = false
                },
                20 => new AccountWithLeadOutputModel()
                {
                    Id = 20,
                    LeadId = 5,
                    FirstName = "Sergei",
                    LastName = "Piratov",
                    Patronymic = "Nikolaevich",
                    Phone = "+79265555555",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "RUB",
                    CurrencyName = "RussianRuble",
                    IsDeleted = false
                },

                21 => new AccountWithLeadOutputModel()
                {
                    Id = 21,
                    LeadId = 1,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Patronymic = "Nikolaevna",
                    Phone = "+79261111111",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 5",
                    BirthDate = "01.01.1970",
                    LeadIsDeleted = false,
                    CurrencyCode = "EUR",
                    CurrencyName = "Euro",
                    IsDeleted = false
                },
                22 => new AccountWithLeadOutputModel()
                {
                    Id = 22,
                    LeadId = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivanovna",
                    Phone = "+79267777777",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    LeadIsDeleted = false,
                    CurrencyCode = "USD",
                    CurrencyName = "USDollar",
                    IsDeleted = false
                },
                23 => "Choose currency",
                _ => -1,
            };
        }
    }
}
