using CRM.API.Models.Output;
using System.Collections.Generic;

namespace CRM.NUnitTest
{
    public class OutputDataMocksForLeads
    {
        public dynamic GetLeadOutputModelMockById(int leadId)  //done
        {
            return leadId switch
            {
                1 => new LeadOutputModel()
                {
                    Id = 11,
                    FirstName = "Anna",
                    LastName = "Yazikova",
                    Login = "annayazikova2456388",
                    Phone = "+79314554545",                 
                    City = "Saratov",                    
                    BirthDate = "16.05.1991"                    
                },
                2 => new LeadOutputModel()
                {
                    Id = 12,
                    FirstName = "Milena",
                    LastName = "Murashova",                    
                    Login = "AnnaMurashka7639",                    
                    Phone = "+79762457628",                    
                    City = "Minsk",                    
                    BirthDate = "17.03.1990"
                },
                3 => new LeadOutputModel()
                {
                    Id = 13,
                    FirstName = "Zena",
                    LastName = "Koroleva",                    
                    Login = "Zena7639",                   
                    Phone = "+79762457633",                   
                    City = "Minsk",                   
                    BirthDate = "17.03.1998"
                },
                4 => "Enter the name",                
                5 => "Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_",               
                6 => "User with this email already exists",                
                _ => new LeadOutputModel(),
            };
        }

        public List<LeadOutputModel> GetListOfLeadOutputModelsMockById(int num)   //done
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
                        Login = "AlenaNurashka7639",
                        Phone = "+79261111111",                        
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
                        }
                },
                2 => new List<LeadOutputModel>()
                {
                        new LeadOutputModel()
                        {
                            Id = 4,
                            FirstName = "Ivan",
                            LastName = "Piratov",                            
                            Login = "IvashkaNurashkaaaaa7639",
                            Phone = "+79324444444",                            
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
                                Login = "GalichVladimir1965",
                                Phone = "+79268888888",                                
                                BirthDate = "11.04.1965",                                
                                Accounts = new List<AccountOutputModel>()
                                {
                                    new AccountOutputModel()
                                    {
                                        Id = 13,
                                        IsDeleted = false,
                                        CurrencyCode = "EUR",
                                        CurrencyName = "Euro"
                                    },
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
                            Id = 8,
                            FirstName = "Oksana",
                            LastName = "Galich",                            
                            Login = "GalichOksana1965",
                            Phone = "+79269999999",                            
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
                            Login = "AlenaNurashka7639",
                            Phone = "+79261111111",                            
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
                                Login = "GalaVlada1969",
                                Phone = "+79268888887",                                
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
                            Login = "PashkaNurashka7639",
                            Phone = "+79322222222",                        
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
                            Login = "AlenaNurashka7639",
                            Phone = "+79261111111",                           
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
                            Id = 4,
                            FirstName = "Sergei",
                            LastName = "Piratov",                            
                            Login = "IvashkaNurashka7639",
                            Phone = "+79265555555",                           
                            BirthDate = "01.12.1997",                            
                            Accounts = new List<AccountOutputModel>()
                            {
                                new AccountOutputModel()
                                {
                                    Id = 9,
                                    IsDeleted = false,
                                    CurrencyCode = "JPY",
                                    CurrencyName = "Yen"
                                },
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
                                Login = "GalichVladimir1965",
                                Phone = "+79268888888",                                
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
                                Login = "GalaVlada1969",
                                Phone = "+79268888887",                                
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

    }
}
