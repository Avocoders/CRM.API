using CRM.API.Models.Input;
using CRM.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.NUnitTest
{
    public class LeadInputMock
    {
        public LeadInputModel CreateLeadMock(int num)
        {
            switch (num)
            {
                case 1:
                    return new LeadInputModel()
                    {

                        FirstName = "Anna",
                        LastName = "Yazikova",
                        Patronymic = "Ivannova",
                        Login = "annayazikova2456388",
                        Password = "Samolettu356",
                        Phone = "+79314554545",
                        Email = "annayazikova2456388@gmail.com",
                        CityId = 2,
                        Address = "Malaya Konyushennaya Ulitsa229",
                        BirthDate = "16.05.1991"
                    };break;
                case 2:
                    return new LeadInputModel()
                    {
                        FirstName = "Anna",
                        LastName = "Murashova",
                        Patronymic = "Nikolaevna",
                        Login = "AnnaMurashka7639",
                        Password = "AnMuNi867594",
                        Phone = "+79762457628",
                        Email = "annamurashova@gmail.com",
                        CityId = 3,
                        Address = "Kaliningradskaya, 35, 15",
                        BirthDate = "17.03.1990"
                    };break;
            }
            return new LeadInputModel();
        }
    }
}