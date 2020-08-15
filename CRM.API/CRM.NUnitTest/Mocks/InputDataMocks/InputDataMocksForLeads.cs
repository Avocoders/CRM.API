using CRM.API.Models.Input;

namespace CRM.NUnitTest
{
    public class InputDataMocksForLeads
    {
        public dynamic GetLeadInputModelMock(int num)
        {
            return num switch
            {
                1 => new LeadInputModel()
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
                },
                2 => new LeadInputModel()
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
                },
                3 => new LeadInputModel()
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
                },
                4 => new LeadInputModel()
                {
                    FirstName = "Nastya",
                    LastName = "Ivleeva",
                    Patronymic = "Petrovna",
                    Login = "NastyshaAgon7639",
                    Password = "NasNas94",
                    Phone = "+79762443633",
                    Email = "nasnas493@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 30, 6",
                    BirthDate = "17.03.1989"
                },
                5 => new LeadInputModel()
                {
                    FirstName = "Alberta",
                    LastName = "Koval",
                    Patronymic = "Nikolaevna",
                    Login = "Berta435",
                    Password = "Berta435533",
                    Phone = "+79762457634",
                    Email = "bertalove4@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 10",
                    BirthDate = "17.03.1993"
                },
                6 => new LeadInputModel()
                {
                    FirstName = "Ida",
                    LastName = "Galich",
                    Patronymic = "Eugenievna",
                    Login = "Galichchch7463",
                    Password = "Idonchikchik648",
                    Phone = "+79762457493",
                    Email = "idagalich9473@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 5, 5",
                    BirthDate = "17.05.1968"
                },
                _ => new LeadInputModel(),
            };
        }

        public LeadInputModel UpdateLeadMock(int num)
        {
            return num switch
            {
                11 => new LeadInputModel()
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
                },
                12 => new LeadInputModel()
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
                },
                13 => new LeadInputModel()
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
                },
                _ => new LeadInputModel(),
            };
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