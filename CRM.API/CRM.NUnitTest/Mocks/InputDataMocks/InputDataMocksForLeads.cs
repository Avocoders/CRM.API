using CRM.API.Models.Input;
using CRM.Core;

namespace CRM.NUnitTest
{
    public class InputDataMocksForLeads
    {      
        public LeadInputModel GetLeadInputModelMock(int num)   
        {
            return num switch
            {
                1 => new LeadInputModel()
                {
                    FirstName = "Alena",
                    LastName = "Nuratova",
                    Patronymic = "Ivannova",
                    Login = "Pupsik7464",
                    Password = "Samolettu356",
                    Phone = "+793145545457",
                    Email = "annayazikova2456388@gmail.com",
                    CityId = 11,
                    Address = "Malaya Konyushennaya Ulitsa229",
                    BirthDate = "16.05.1991"
                },
                2 => new LeadInputModel()
                {
                    FirstName = "Pavel",
                    LastName = "Muratov",
                    Patronymic = "Nikolaevich",
                    Login = "PavelPavel94",
                    Password = "AnMuNi867594",
                    Phone = "+79762457628",
                    Email = "annamurashova@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1990"
                },
                3 => new LeadInputModel()
                {
                    FirstName = "Elenka",
                    LastName = "Galich",
                    Patronymic = "Nikolaevna",
                    Login = "Pupsichek7464",      
                    Password = "Zena867594",
                    Phone = "+79762457633",
                    Email = "zena7639@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1998"
                },
                4 => new LeadInputModel()
                {
                    FirstName = "Ivan",
                    LastName = "",                 
                    Patronymic = "Nikolaevna",
                    Login = "Ivago948",
                    Password = "Zena867594",
                    Phone = "+79762457633",
                    Email = "zena7639@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1998"
                },
                5 => new LeadInputModel()
                {
                    FirstName = "Sergei",
                    LastName = "Piratov",
                    Patronymic = "Nikolaevna",
                    Login = "Sergooo937",
                    Password = "Zena867594",
                    Phone = "+79762457633",
                    Email = "zena7639@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = ""               
                },
                6 => new LeadInputModel()
                {
                    FirstName = "Zena",
                    LastName = "Koroleva",
                    Patronymic = "Nikolaevna",
                    Login = "Pupsik7464",    
                    Password = "Zena867594",
                    Phone = "+79762457633",
                    Email = "zena7639@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 15",
                    BirthDate = "17.03.1998"
                },
                11 => new LeadInputModel()
                {                 
                    FirstName = "Daria",
                    LastName = "Piratova",
                    Patronymic = "Ivannova",
                    Login = "dashunya7363",
                    Password = "Samolettu356",
                    Phone = "+79314647493",
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
                14 => new LeadInputModel()
                {
                    FirstName = "",
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
                15 => new LeadInputModel()
                {
                    FirstName = "Alberta",
                    LastName = "Koval",
                    Patronymic = "Nikolaevna",
                    Login = "Berta435",
                    Password = "berta",
                    Phone = "+79762457634",
                    Email = "bertalove4@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 35, 10",
                    BirthDate = "17.03.1993"
                },
                16 => new LeadInputModel()
                {
                    FirstName = "Ida",
                    LastName = "Galich",
                    Patronymic = "Eugenievna",
                    Login = "Galichchch7463",
                    Password = "Idonchikchik648",
                    Phone = "+79762457493",
                    Email = "zena7639@gmail.com",
                    CityId = 14,
                    Address = "Kaliningradskaya, 5, 5",
                    BirthDate = "17.05.1968"
                },
                _ => new LeadInputModel(),
            };
        }

        public SearchParametersInputModel SearchInputMock(int num)  
        {
            
            return num switch
            {
                1 => new SearchParametersInputModel()
                {
                    FirstNameSearchMode = (int)SearchMode.ExactValue, 
                    FirstName = "Alena"
                },
                2 => new SearchParametersInputModel()
                {
                    PhoneSearchMode = (int)SearchMode.StartWithValue,
                    Phone = "+793244"
                },
                3 => new SearchParametersInputModel()
                {
                    LoginSearchMode = (int)SearchMode.ContainsValue,
                    Login = "Galich"
                },
                4 => new SearchParametersInputModel()
                {
                    BirthDateBegin = "01.01.1969",
                    BirthDateEnd = "11.01.1970"
                },
                5 => new SearchParametersInputModel() 
                {
                    AccountId = 5
                },
                6 => new SearchParametersInputModel()
                {
                    CurrencyId = 1
                },
                _ => new SearchParametersInputModel(),
            };
        }

        public EmailInputModel GetEmailInputModelMockByLeadId(int num) 
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
                    Email = "voron5@gmail.com"
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
                _ => new EmailInputModel(),
            };
        }
    }
}