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
                case 256:
                    return new LeadOutputModel()
                    {
                        Id = 256,
                        FirstName = "Viktor",
                        LastName = "Malyshev",
                        Patronymic = "Grigorievich",
                        Login = "ViktorMalyshev5946357064",
                        Phone = "+72963050540",
                        Email = "malyshevvictor623@gmail.com",
                        Address = "Malaya Konyushennaya Ulitsa229",
                        BirthDate = "17.05.1978",
                        RegistrationDate = "27.07.2011 00:00:00",
                        ChangeDate = "29.07.2020 21:14:21",
                        Role = "Client",
                        City = "Gatchina",
                        Accounts = new List<AccountOutputModel>()
                            {
                                    new AccountOutputModel()
                                    {
                                        Id = 20859,
                                        IsDeleted = false,
                                        CurrencyCode = "EUR",
                                        CurrencyName = "Euro"
                                    },

                                    new AccountOutputModel()
                                    {

                                        Id = 36942,
                                        IsDeleted = false,
                                        CurrencyCode = "RUB",
                                        CurrencyName = "RussianRuble"
                                    },
                                     new AccountOutputModel()
                                     {
                                        Id = 194231,
                                        IsDeleted = false,
                                        CurrencyCode = "JPY",
                                        CurrencyName = "Yen"

                                      },

                                     new AccountOutputModel()
                                     {
                                            Id = 290819,
                                            IsDeleted = false,
                                            CurrencyCode = "RUB",
                                            CurrencyName = "RussianRuble"

                                     },
                                      new AccountOutputModel()
                                      {
                                          Id = 486624,
                                        IsDeleted = false,
                                        CurrencyCode = "JPY",
                                        CurrencyName = "Yen"

                                      },
                                      new AccountOutputModel()
                                      {
                                           Id = 510061,
                                            IsDeleted = false,
                                            CurrencyCode = "RUB",
                                            CurrencyName = "RussianRuble"

                                      },
                                      new AccountOutputModel()
                                      {
                                        Id = 647787,
                                        IsDeleted = false,
                                        CurrencyCode = "USD",
                                        CurrencyName = "USDollar"

                                      },
                                      new AccountOutputModel()
                                      {
                                        Id = 954438,
                                        IsDeleted = false,
                                        CurrencyCode = "JPY",
                                        CurrencyName = "Yen"

                                      },
                                      new AccountOutputModel()
                                      {
                                        Id = 988827,
                                        IsDeleted = false,
                                        CurrencyCode = "JPY",
                                        CurrencyName = "Yen"

                                      }
                        },
                        IsDeleted = false
                    }; break;
                case 2:
                    return new LeadOutputModel()
                    {
                        Id = 2,
                        FirstName = "Angelina",
                        LastName = "Stasova",
                        Patronymic = "Benidiktovna",
                        Login = "AngelinaStasova8912731760",
                        Phone = "+75983349112",
                        Email = "AngelinaStasova8912731760@gmail.com",
                        Address = "2nd Duck Street3533",
                        BirthDate = "17.03.1990",
                        RegistrationDate = "25.03.2011 00:00:00",
                        ChangeDate = "29.07.2020 21:14:21",
                        Role = "Client",
                        City = "Izhevsk",
                        Accounts = new List<AccountOutputModel>()
                {
                    new AccountOutputModel()
                    {
                         Id = 1121,
                        IsDeleted = false,
                        CurrencyCode = "EUR",
                        CurrencyName = "Euro"
                    },
                    new AccountOutputModel()
                    {
                        Id = 173823,
                        IsDeleted = false,
                        CurrencyCode = "USD",
                        CurrencyName = "USDollar"
                    },
                    new AccountOutputModel()
                    {
                        Id = 177733,
                        IsDeleted = false,
                        CurrencyCode = "EUR",
                        CurrencyName = "Euro"
                    },
                    new AccountOutputModel()
                    {
                        Id = 299969,
                        IsDeleted = false,
                        CurrencyCode = "RUB",
                        CurrencyName = "RussianRuble"
                    },
                    new AccountOutputModel()
                    {
                        Id = 369888,
                        IsDeleted = false,
                        CurrencyCode = "USD",
                        CurrencyName = "USDollar"
                    },
                    new AccountOutputModel()
                    {
                        Id = 383426,
                        IsDeleted = false,
                        CurrencyCode = "RUB",
                        CurrencyName = "RussianRuble"
                    },
                    new AccountOutputModel()
                    {
                         Id = 539209,
                        IsDeleted = false,
                        CurrencyCode = "EUR",
                        CurrencyName = "Euro"
                    },
                    new AccountOutputModel()
                    {
                        Id = 665817,
                        IsDeleted = false,
                        CurrencyCode = "USD",
                        CurrencyName = "USDollar"
                    },
                    new AccountOutputModel()
                    {
                        Id = 668857,
                        IsDeleted = false,
                        CurrencyCode = "RUB",
                        CurrencyName = "RussianRuble"
                    }
                },

                        IsDeleted = false
                    }; break;

            }
            return new LeadOutputModel();
        }

        //public long CreateLeadMock(LeadOutputModel leadinputmodel)
        //{
        //    switch (leadId)
        //    {
        //        case 256:
        //    }

        //}
    }
}
