using CRM.API.Models.Output;
using System.Collections.Generic;

namespace CRM.NUnitTest
{
    public class LeadOutputModelMocks

    {
        public LeadOutputModel GetLeadMockById(int leadId)
        {
            return leadId switch
            {
                1 => new LeadOutputModel()
                {
                    Id = 1,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Patronymic = "Nikolaevna",
                    Login = "AlenaNurashka7639",
                    Phone = "+79261111111",
                    Email = "alenanuratova@gmail.com",
                    Address = "Kaliningradskaya, 25, 5",
                    BirthDate = "01.01.1970",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Moscow",
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
                2 => new LeadOutputModel()
                {
                    Id = 2,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    Patronymic = "Nikolaevich",
                    Login = "PashkaNurashka7639",
                    Phone = "+79322222222",
                    Email = "pashkamuratov@gmail.com",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.08.1995",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Moscow",
                    Accounts = new List<AccountOutputModel>()
                    {
                         new AccountOutputModel()
                         {
                             Id = 2,
                             IsDeleted = false,
                             CurrencyCode = "USD",
                             CurrencyName = "USDollar"
                         },
                         new AccountOutputModel()
                         {
                             Id = 11,
                             IsDeleted = false,
                             CurrencyCode = "EUR",
                             CurrencyName = "Euro"
                         },
                    },
                    IsDeleted = false
                },
                3 => new LeadOutputModel()
                {
                    Id = 3,
                    FirstName = "Elena",
                    LastName = "Galich",
                    Patronymic = "Ivanovna",
                    Login = "Elenaera1978",
                    Phone = "+79263333333",
                    Email = "elenagalich@gmail.com",
                    Address = "Stroitelei, 13, 78",
                    BirthDate = "11.04.1980",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Saratov",
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
                },
                4 => new LeadOutputModel()
                {
                    Id = 4,
                    FirstName = "Ivan",
                    LastName = "Piratov",
                    Patronymic = "Nikolaevich",
                    Login = "IvashkaNurashkaaaaa7639",
                    Phone = "+79324444444",
                    Email = "pirat@gmail.com",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Minsk",
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
                5 => new LeadOutputModel()
                {
                    Id = 5,
                    FirstName = "Sergei",
                    LastName = "Piratov",
                    Patronymic = "Nikolaevich",
                    Login = "IvashkaNurashka7639",
                    Phone = "+79265555555",
                    Email = "piratov@gmail.com",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Minsk",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 5,
                            IsDeleted = false,
                            CurrencyCode = "RUB",
                            CurrencyName = "RussianRuble"
                        },
                    },
                    IsDeleted = false
                },
                6 => new LeadOutputModel()
                {
                    Id = 6,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivanovna",
                    Login = "Piratova1980",
                    Phone = "+79267777777",
                    Email = "piratovadaria@gmail.com",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Minsk",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 6,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                7 => new LeadOutputModel()
                {
                    Id = 7,
                    FirstName = "Vladimir",
                    LastName = "Galich",
                    Patronymic = "Ivanovich",
                    Login = "GalichVladimir1965",
                    Phone = "+79268888888",
                    Email = "galivova@gmail.com",
                    Address = "Stroitelei, 13, 78",
                    BirthDate = "11.04.1965",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Saratov",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 7,
                            IsDeleted = false,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                    },
                    IsDeleted = false
                },
                8 => new LeadOutputModel()
                {
                    Id = 8,
                    FirstName = "Oksana",
                    LastName = "Galich",
                    Patronymic = "Dmitrievna",
                    Login = "GalichOksana1965",
                    Phone = "+79269999999",
                    Email = "galichvova@gmail.com",
                    Address = "Stroitelei, 13, 78",
                    BirthDate = "11.04.1965",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Saratov",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 8,
                            IsDeleted = false,
                            CurrencyCode = "JPY",
                            CurrencyName = "Yen"
                        },
                    },
                    IsDeleted = false
                },
                9 => new LeadOutputModel()
                {
                    Id = 9,
                    FirstName = "Vlada",
                    LastName = "Gala",
                    Patronymic = "Ivanovna",
                    Login = "GalaVlada1969",
                    Phone = "+79268888887",
                    Email = "gala@gmail.com",
                    Address = "Stroitelei, 13, 9",
                    BirthDate = "11.04.1969",
                    RegistrationDate = "02.01.2020 00:00:00",
                    ChangeDate = "02.01.2020 00:00:00",
                    Role = "Client",
                    City = "Saratov",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 9,
                            IsDeleted = false,
                            CurrencyCode = "RUB",
                            CurrencyName = "RussianRuble"
                        },
                    },
                    IsDeleted = false
                },
                10 => new LeadOutputModel()
                {
                    Id = 10,
                    FirstName = "Oksi",
                    LastName = "Miron",
                    Patronymic = "Dmitrievich",
                    Login = "Oksi1965",
                    Phone = "+79269999955",
                    Email = "oksimiron@gmail.com",
                    Address = "Stroitelei, 13, 70",
                    BirthDate = "12.04.1965",
                    RegistrationDate = "01.01.2020 00:00:00",
                    ChangeDate = "01.01.2020 00:00:00",
                    Role = "Client",
                    City = "Saratov",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                            Id = 10,
                            IsDeleted = false,
                            CurrencyCode = "USD",
                            CurrencyName = "USDollar"
                        },
                    },
                    IsDeleted = false
                },
                11 => new LeadOutputModel()
                {
                    Id = 11,
                    FirstName = "Anna",
                    LastName = "Yazikova",
                    Patronymic = "Ivannova",
                    Login = "annayazikova2456388",
                    Phone = "+79314554545",
                    Email = "annayazikova2456388@gmail.com",
                    Address = "Malaya Konyushennaya Ulitsa229",
                    BirthDate = "16.05.1991",
                    RegistrationDate = null,
                    ChangeDate = null,
                    Role = "Client",
                    City = "Saratov",
                    Accounts = null,
                    IsDeleted = false
                },
                12 => new LeadOutputModel()
                {
                    Id = 12,
                    FirstName = "Milena",
                    LastName = "Murashova",
                    Patronymic = "Nikolaevna",
                    Login = "AnnaMurashka7639",
                    Phone = "+79762457628",
                    Email = "annamurashova@gmail.com",
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1990",
                    RegistrationDate = null,
                    ChangeDate = null,
                    Role = "Client",
                    City = "Minsk",
                    Accounts = null,
                    IsDeleted = false
                },
                13 => new LeadOutputModel()
                {
                    Id = 13,
                    FirstName = "Zena",
                    LastName = "Koroleva",
                    Patronymic = "Nikolaevna",
                    Login = "Zena7639",
                    Phone = "+79762457633",
                    Email = "Zena7639@gmail.com",
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1998",
                    RegistrationDate = null,
                    ChangeDate = null,
                    Role = "Client",
                    City = "Minsk",
                    Accounts = null,
                    IsDeleted = false
                },
                _ => new LeadOutputModel(),
            };
        }

        public string GetEmailByLeadId(int num)
        {
            return num switch
            {
                2 => "voron2@gmail.com",
                3 => "voron3@gmail.com",
                5 => "voron5@gmail.com",
                6 => "voron6@gmail.com",
                7 => "Enter the email",
                8 => "The Email is incorrect",
                10 => "User with this email already exists",
                _ => "error",
            };
        }
        public  List<LeadOutputModel> SearchParametersMock(int num)
        {
            return num switch
            {
                1 => new List<LeadOutputModel>()
                {
                        new LeadOutputModel()
                        {
                        Id = 1,
                        FirstName = "Alena",
                        LastName = "Nuratova",
                        Patronymic = "Nikolaevna",
                        Login = "AlenaNurashka7639",
                        Phone = "+79261111111",
                        Email = "alenanuratova@gmail.com",
                        Address = "Kaliningradskaya, 25, 5",
                        BirthDate = "01.01.1970",
                        RegistrationDate = "01.01.2020 00:00:00",
                        ChangeDate = "01.01.2020 00:00:00",
                        Role = "Client",
                        City = "Moscow",
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
                        }
                },
                2 => new List<LeadOutputModel>()
                { 
                        new LeadOutputModel()
                        {
                            Id = 4,
                            FirstName = "Ivan",
                            LastName = "Piratov",
                            Patronymic = "Nikolaevich",
                            Login = "IvashkaNurashkaaaaa7639",
                            Phone = "+79324444444",
                            Email = "pirat@gmail.com",
                            Address = "Kaliningradskaya, 25, 10",
                            BirthDate = "01.12.1997",
                            RegistrationDate = "01.01.2020 00:00:00",
                            ChangeDate = "01.01.2020 00:00:00",
                            Role = "Client",
                            City = "Minsk",
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
                        }
                },
                3 => new List<LeadOutputModel>()
                {
                         new LeadOutputModel()
                         {
                                Id = 7,
                                FirstName = "Vladimir",
                                LastName = "Galich",
                                Patronymic = "Ivanovich",
                                Login = "GalichVladimir1965",
                                Phone = "+79268888888",
                                Email = "galivova@gmail.com",
                                Address = "Stroitelei, 13, 78",
                                BirthDate = "11.04.1965",
                                RegistrationDate = "01.01.2020 00:00:00",
                                ChangeDate = "01.01.2020 00:00:00",
                                Role = "Client",
                                City = "Saratov",
                                Accounts = new List<AccountOutputModel>()
                                {
                                    new AccountOutputModel()
                                    {
                                        Id = 7,
                                        IsDeleted = false,
                                        CurrencyCode = "EUR",
                                        CurrencyName = "Euro"
                                    },
                                },
                                IsDeleted = false
                         },
                         new LeadOutputModel()
                         {
                            Id = 8,
                            FirstName = "Oksana",
                            LastName = "Galich",
                            Patronymic = "Dmitrievna",
                            Login = "GalichOksana1965",
                            Phone = "+79269999999",
                            Email = "galichvova@gmail.com",
                            Address = "Stroitelei, 13, 78",
                            BirthDate = "11.04.1965",
                            RegistrationDate = "01.01.2020 00:00:00",
                            ChangeDate = "01.01.2020 00:00:00",
                            Role = "Client",
                            City = "Saratov",
                            Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                {
                                    Id = 8,
                                    IsDeleted = false,
                                    CurrencyCode = "JPY",
                                    CurrencyName = "Yen"
                                },
                            },
                            IsDeleted = false
                         }
                },
                4 => new List<LeadOutputModel>()
                {
                         new LeadOutputModel()
                         {
                            Id = 1,
                            FirstName = "Alena",
                            LastName = "Nuratova",
                            Patronymic = "Nikolaevna",
                            Login = "AlenaNurashka7639",
                            Phone = "+79261111111",
                            Email = "alenanuratova@gmail.com",
                            Address = "Kaliningradskaya, 25, 5",
                            BirthDate = "01.01.1970",
                            RegistrationDate = "01.01.2020 00:00:00",
                            ChangeDate = "01.01.2020 00:00:00",
                            Role = "Client",
                            City = "Moscow",
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
                         new LeadOutputModel()
                         {
                                Id = 9,
                                FirstName = "Vlada",
                                LastName = "Gala",
                                Patronymic = "Ivanovna",
                                Login = "GalaVlada1969",
                                Phone = "+79268888887",
                                Email = "gala@gmail.com",
                                Address = "Stroitelei, 13, 9",
                                BirthDate = "11.04.1969",
                                RegistrationDate = "02.01.2020 00:00:00",
                                ChangeDate = "02.01.2020 00:00:00",
                                Role = "Client",
                                City = "Saratov",
                                Accounts = new List<AccountOutputModel>()
                                {
                                    new AccountOutputModel()
                                    {
                                            Id = 9,
                                            IsDeleted = false,
                                            CurrencyCode = "RUB",
                                            CurrencyName = "RussianRuble"
                                    },
                                },
                                IsDeleted = false
                         }
                },
                _ => new List<LeadOutputModel>(),
            };
        }
    }
}
