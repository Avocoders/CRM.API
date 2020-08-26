using CRM.API.Models.Output;
using CRM.Data.DTO;
using System.Collections.Generic;

namespace CRM.NUnitTest
{
    public class OutputDataMocksForLeads
    {
        public dynamic GetLeadOutputModelMockById(int leadId)   
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
                    Role = "Client",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 5",
                    BirthDate = "01.01.1970",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {                           
                        },
                        new AccountOutputModel()
                        {                            
                        },
                        new AccountOutputModel()
                        {                            
                        }
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
                    Role = "Client",
                    City = "Moscow",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.08.1995",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
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
                    Role = "Client",
                    City = "Saratov",
                    Address = "Stroitelei, 13, 78",
                    BirthDate = "11.04.1980",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        }                       
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
                    Role = "Client",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
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
                    Role = "Client",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
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
                    Role = "Client",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 25, 10",
                    BirthDate = "01.12.1997",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        },
                    },
                    IsDeleted = false
                },
                11 => new LeadOutputModel()
                {
                    Id = 11,
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivannova",
                    Login = "dashunya7363",                   
                    Phone = "+79314647493",
                    Email = "annayazikova2456388@gmail.com",
                    Role = "Client",
                    City = "Saratov",                  
                    Address = "Malaya Konyushennaya Ulitsa229",
                    BirthDate = "16.05.1991",
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
                    Role = "Client",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1990",
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
                    Email = "zena7639@gmail.com",
                    Role = "Client",
                    City = "Minsk",
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1998",
                    IsDeleted = false
                },
                14 => "Enter the name",                
                15 => "Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_",               
                16 => "User with this email already exists",                
                _ => new LeadOutputModel(),
            };
        }

        public List<LeadOutputModel> GetListOfLeadOutputModelsMockById(int num)   
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
                        Role = "Client",
                        City = "Moscow",
                        Address = "Kaliningradskaya, 25, 5",
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
                            Role = "Client",
                            City = "Minsk",
                            Address = "Kaliningradskaya, 25, 10",
                            BirthDate = "01.12.1997",
                            Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                {
                                    Id = 8,
                                    IsDeleted = false,
                                    CurrencyCode = "JPY",
                                    CurrencyName = "Yen"
                                }
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
                                Role = "Client",
                                City = "Saratov",
                                Address = "Stroitelei, 13, 78",
                                BirthDate = "11.04.1965",                                
                                Accounts = new List<AccountOutputModel>()
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
                                        Id = 1,
                                        IsDeleted = false,
                                        CurrencyCode = "RUB",
                                        CurrencyName = "RussianRubble"
                                    }
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
                            Role = "Client",
                            City = "Saratov",
                            Address = "Stroitelei, 13, 78",
                            BirthDate = "11.04.1965",                           
                            Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                {
                                    Id = 15,
                                    IsDeleted = false,
                                    CurrencyCode = "JPY",
                                    CurrencyName = "Yen"
                                }
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
                            Role = "Client",
                            City = "Moscow",
                            Address = "Kaliningradskaya, 25, 5",
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
                                        new AccountOutputModel()
                                        {
                                            Id = 2,
                                            IsDeleted = false,
                                            CurrencyCode = "USD",
                                            CurrencyName = "USDollar"
                                        },
                                        new AccountOutputModel()
                                        {
                                            Id = 1,
                                            IsDeleted = false,
                                            CurrencyCode = "USD",
                                            CurrencyName = "USDollar"
                                        }
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
                                Role = "Client",
                                City = "Saratov",
                                Address = "Stroitelei, 13, 9",
                                BirthDate = "11.04.1969",                               
                                Accounts = new List<AccountOutputModel>()
                                {
                                    new AccountOutputModel()
                                    {
                                            Id = 16,
                                            IsDeleted = false,
                                            CurrencyCode = "RUB",
                                            CurrencyName = "RussianRuble"
                                    },
                                    new AccountOutputModel()
                                    {
                                            Id = 17,
                                            IsDeleted = false,
                                            CurrencyCode = "JPY",
                                            CurrencyName = "Yen"
                                    }
                                },
                                IsDeleted = false
                         }
                },
                5 => new List<LeadOutputModel>()
                {
                        new LeadOutputModel()
                        {
                            Id = 2,
                            FirstName = "Pavel",
                            LastName = "Muratov",
                            Patronymic = "Nikolaevich",
                            Login = "PashkaNurashka7639",
                            Phone = "+79322222222",
                            Email = "pashkamuratov@gmail.com",
                            Role = "Client",
                            City = "Moscow",
                            Address = "Kaliningradskaya, 25, 10",
                            BirthDate = "01.08.1995",
                            Accounts = new List<AccountOutputModel>()
                            {                            
                                new AccountOutputModel()
                                {
                                    Id = 5,
                                    IsDeleted = false,
                                    CurrencyCode = "EUR",
                                    CurrencyName = "Euro"
                                },
                            },
                            IsDeleted = false
                        }
                },
                6 => new List<LeadOutputModel>()
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
                            Role = "Client",
                            City = "Moscow",
                            Address = "Kaliningradskaya, 25, 5",
                            BirthDate = "01.01.1970",
                            Accounts = new List<AccountOutputModel>()
                            {
                                        new AccountOutputModel()
                                        {
                                            Id = 1,
                                            IsDeleted = false,
                                            CurrencyCode = "RUB",
                                            CurrencyName = "RussianRuble"
                                        }
                            },
                            IsDeleted = false
                         },
                         new LeadOutputModel()
                         {
                            Id = 5,
                            FirstName = "Sergei",
                            LastName = "Piratov",
                            Patronymic = "Nikolaevich",
                            Login = "IvashkaNurashka7639",
                            Phone = "+79265555555",
                            Email = "piratov@gmail.com",
                            Role = "Client",
                            City = "Minsk",
                            Address = "Kaliningradskaya, 25, 10",
                            BirthDate = "01.12.1997",
                            Accounts = new List<AccountOutputModel>()
                            {                                
                                new AccountOutputModel()
                                {
                                    Id = 20,
                                    IsDeleted = false,
                                    CurrencyCode = "RUB",
                                    CurrencyName = "RussianRubble"
                                }
                            },
                            IsDeleted = false
                         },
                         new LeadOutputModel()
                         {
                                Id = 7,
                                FirstName = "Vladimir",
                                LastName = "Galich",
                                Patronymic = "Ivanovich",
                                Login = "GalichVladimir1965",
                                Phone = "+79268888888",
                                Email = "galivova@gmail.com",
                                Role = "Client",
                                City = "Saratov",
                                Address = "Stroitelei, 13, 78",
                                BirthDate = "11.04.1965",
                                Accounts = new List<AccountOutputModel>()
                                {                                    
                                    new AccountOutputModel()
                                    {
                                        Id = 14,
                                        IsDeleted = false,
                                        CurrencyCode = "RUB",
                                        CurrencyName = "RussianRubble"
                                    }
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
                                Role = "Client",
                                City = "Saratov",
                                Address = "Stroitelei, 13, 9",
                                BirthDate = "11.04.1969",
                                Accounts = new List<AccountOutputModel>()
                                {
                                    new AccountOutputModel()
                                    {
                                            Id = 16,
                                            IsDeleted = false,
                                            CurrencyCode = "RUB",
                                            CurrencyName = "RussianRuble"
                                    }
                                },
                                IsDeleted = false
                         }
                },
                _ => new List<LeadOutputModel>(),
            };
        }

        public string GetEmailByLeadId(int num)  
        {
            return num switch
            {
                2 => "E-mail was updated",
                3 => "E-mail was updated",
                5 => "E-mail was updated",
                6 => "User with this email already exists",
                7 => "Enter the email",
                8 => "The Email is incorrect",                
                _ => "error",
            };
        }

        public dynamic GetLeadOutputModelAfterUpdateMockById(int leadId)   
        {
            return leadId switch
            {
                1 => new LeadOutputModel()
                {
                    Id = 1,
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Login = "Pupsik7464",
                    Phone = "+793145545457",
                    City = "Moscow",
                    BirthDate = "16.05.1991",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        }
                    },
                    IsDeleted = false
                },
                2 => new LeadOutputModel()
                {
                    Id = 2,
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    Login = "PavelPavel94",
                    Phone = "+79762457628",
                    City = "Moscow",
                    BirthDate = "17.03.1990",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        },
                    },
                    IsDeleted = false
                },
                3 => new LeadOutputModel()
                {
                    Id = 3,
                    FirstName = "Elenka",
                    LastName = "Galich",
                    Login = "Pupsichek7464",
                    Phone = "+79762457633",
                    City = "Saratov",
                    BirthDate = "17.03.1998",
                    Accounts = new List<AccountOutputModel>()
                    {
                        new AccountOutputModel()
                        {
                        },
                        new AccountOutputModel()
                        {
                        }
                    },
                    IsDeleted = false
                },                
                4 => "Enter the last name",
                5 => "Enter the date of birth",
                6 => "User with this login already exists",
                _ => new LeadOutputModel(),
            };
        }
    }
}
