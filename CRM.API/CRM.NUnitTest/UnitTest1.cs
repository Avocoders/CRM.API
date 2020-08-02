using NUnit.Framework;
using System.Net.Http;
using CRM.API;
using CRM.API.Models.Input;
using Newtonsoft.Json;
using System.Text;
using CRM.API.Models.Output;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using CRM.Data;
using Autofac.Extensions.DependencyInjection;

namespace CRM.NUnitTest
{
    public class Tests
    {
        [Test]


        public async Task TestMethod1()
        {
            var webHostBuilder =
                  new WebHostBuilder()                        
                        .UseEnvironment("Development") // You can set the environment you want (development, staging, production)                           
                        .UseStartup<Startup>(); // Startup class of your web app project
                        

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                string result = await client.GetStringAsync("https://localhost:44382/lead/256");
                Assert.AreEqual("[\"id\":256,\"firstName\":\"Viktor\",\"lastName\":\"Malyshev\",\"patronymic\":\"Grigorievich\",\"login\":\"ViktorMalyshev5946357064\",\"phone\":\" + 72963050540\",\"email\":\"ViktorMalyshev5946357064@gmail.com\",\"address\":\"Malaya Konyushennaya Ulitsa229\",\"birthDate\":\"17.05.1978 0:00:00\",\"registrationDate\":\"27.07.2011 0:00:00\",\"changeDate\":\"29.07.2020 21:14:21\",\"role\":\"Client\",\"city\":\"Gatchina\",\"isDeleted\":false]", result);
                //string result = await client.GetStringAsync("lead/1"); /// из базы вернули стрингу
                //var resultmodel = JsonConvert.DeserializeObject<LeadOutputModel>(result);
                //LeadOutputModel leadoutputmodel = new LeadOutputModel()
                //{
                //    Id = 256,
                //    FirstName = "Viktor",
                //    LastName = "Malyshev",
                //    Patronymic = "Grigorievich",
                //    Login = "ViktorMalyshev5946357064",
                //    Phone = "+72963050540",
                //    Email = "ViktorMalyshev5946357064@gmail.com",
                //    Address = "Malaya Konyushennaya Ulitsa229",
                //    BirthDate = "17.05.1978 0:00:00",
                //    RegistrationDate = "27.07.2011 0:00:00",
                //    ChangeDate = "29.07.2020 21:14:21",
                //    Role = "Client",
                //    City = "Gatchina",
                //    IsDeleted = false

                //};



                //Assert.AreEqual(leadoutputmodel, resultmodel);
            }
        }
    }
}

