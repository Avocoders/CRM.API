using CRM.API.Models.Input;
using CRM.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;
using TransactionStore.API.Models.Input;

namespace CRM.NUnitTest
{
    public class LeadInputMock
    {
        public LeadInputModel CreateLeadMock(int num)
        {
            switch (num)
            {
                case 11:
                    return new LeadInputModel()
                    {

                        FirstName = "Anna",
                        LastName = "Yazikova",
                        Patronymic = "Ivannova",
                        Login = "annayazikova2456388",
                        Password = "Samolettu356",
                        Phone = "+79314554545",
                        Email = "annayazikova2456388@gmail.com",
                        CityId = 11,
                        Address = "Malaya Konyushennaya Ulitsa229",
                        BirthDate = "16.05.1991"
                    }; 
                case 12:
                    return new LeadInputModel()
                    {
                        FirstName = "Milena",
                        LastName = "Murashova",
                        Patronymic = "Nikolaevna",
                        Login = "AnnaMurashka7639",
                        Password = "AnMuNi867594",
                        Phone = "+79762457628",
                        Email = "annamurashova@gmail.com",
                        CityId = 14,
                        Address = "Kaliningradskaya, 35, 15",
                        BirthDate = "17.03.1990"
                    }; 

                case 13:
                    return new LeadInputModel()
                    {
                        FirstName = "Zena",
                        LastName = "Koroleva",
                        Patronymic = "Nikolaevna",
                        Login = "Zena7639",
                        Password = "Zena867594",
                        Phone = "+79762457633",
                        Email = "zena7639@gmail.com",
                        CityId = 14,
                        Address = "Kaliningradskaya, 35, 15",
                        BirthDate = "17.03.1998"
                    };
                    
            }
            return new LeadInputModel();
        }

        public EmailInputModel GetEmailInputModelByLeadId(int num)
        {
            return num switch
            {
                2 => new EmailInputModel()
                {
                    Id = 2,
                    Email = "voron2@gmail.com"
                },
                3 => new EmailInputModel()
                {
                    Id = 3,
                    Email = "voron3@gmail.com"
                },
                5 => new EmailInputModel()
                {
                    Id = 5,
                    Email = "voron5@gmail.com"
                },
                6 => new EmailInputModel()
                {
                    Id = 6,
                    Email = "voron6@gmail.com"
                },
                7 => new EmailInputModel()
                {
                    Id = 7,
                    Email = ""
                },
                8 => new EmailInputModel()
                {
                    Id = 8,
                    Email = "gjfjhfjvjkvfkjvfmail.com"
                },
                10 => new EmailInputModel()
                {
                    Id = 10,
                    Email = "oksimiron@gmail.com"
                },
                _ => new EmailInputModel(),
            };
        }
        public SearchParametersInputModel SearchInputMock(int num)  // вынести в отдельный класс
        {
            return num switch
            {
                1 => new SearchParametersInputModel()
                {
                    FirstNameSearchMode = 1, //перекинуть на enum searchmode
                    FirstName = "Alena"


                },
                2 => new SearchParametersInputModel()
                {
                    PhoneSearchMode = 3,
                    Phone = "+793244"

                },
                3 => new SearchParametersInputModel()
                {
                    LoginSearchMode = 2,
                    Login = "Galich"

                },
                4 => new SearchParametersInputModel()
                {
                    BirthDateBegin = "01.01.1969",
                    BirthDateEnd = "11.01.1970"

                },
                _ => new SearchParametersInputModel(),// добавить поиск по лида по аккаунту
            };
        }
        
    }

}