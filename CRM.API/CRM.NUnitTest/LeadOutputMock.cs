using CRM.API.Models.Output;
using System;
using CRM.Data.DTO;
using System.Collections.Generic;
using System.Text;

namespace CRM.NUnitTest
{
    public class LeadOutputMock

    {
        public LeadOutputModel GetLeadByIdMock(int leadId)
        {
            switch (leadId)
            {
                case 1:
                    return new LeadOutputModel()
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
                    }; break;
       
                   case 2:
                    return new LeadOutputModel()
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
                    }; break;

                case 3:
                    return new LeadOutputModel()
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
                    }; break;

                case 4:
                    return new LeadOutputModel()
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
                    }; break;

                case 5:
                    return new LeadOutputModel()
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
                    }; break;

                case 6:
                    return new LeadOutputModel()
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
                    }; break;
                case 7:
                    return new LeadOutputModel()
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
                    }; break;
                 
                case 8:
                    return new LeadOutputModel()
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
                    }; break;

                case 9:
                  
                    return new LeadOutputModel()
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
                    }; break;

                case 10:

                    return new LeadOutputModel()
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
                    }; break;
            }
            return new LeadOutputModel();
        }

        public string GetEmailByLeadId(int num)
        {
            switch (num)
            {                                   
                case 2: return "voron2@gmail.com";
                case 3: return "voron3@gmail.com";
                case 5: return "voron5@gmail.com";
                case 6: return "voron6@gmail.com";
                case 7: return "Enter the email";
                case 8: return "The Email is incorrect";               
                case 10: return "User with this email already exists";
                    
            }
            return "error";

         
        }
        public  List<LeadOutputModel> SearchParametersMock(int num)
        {
            switch (num)
            {
                case 1:
                    return new List<LeadOutputModel>()
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

                    };
                  case 2:
                    return new List<LeadOutputModel>()

                    { new LeadOutputModel()
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
                    }; 

                case 3:
                    return new List<LeadOutputModel>()
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
                        };
                case 4:
                    return new List<LeadOutputModel>()
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
                        };

            }
            return new List<LeadOutputModel>();
        }
    }
}
