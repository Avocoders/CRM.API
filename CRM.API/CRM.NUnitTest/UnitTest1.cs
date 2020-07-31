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

namespace CRM.NUnitTest
{
    public  class LeadTest: TestSetup
    {
       

        [Test]
        public async Task CreateLead()
        {
            httpClient = new HttpClient
                
                 {
                     BaseAddress = new Uri("http://localhost:44382/lead")
                 };
                                 

            var lead = new LeadDTO()
            {
                FirstName = "Anna",
                LastName = "Konovalova",
                Patronymic = "Ivanovna",
                Password = "konovalova!1989",
                Phone = "+79110000000",
                Email = "annakonovalova@mail.ru",
                CityId = 2,
                Address = "pr.Lenina, h.22-122",
                BirthDate = "09/09/1989"

            };
            var json = JsonConvert.SerializeObject(lead);
            var handleRequest = await httpClient.PostAsync("http://localhost:44382/lead",
                new StringContent(json, Encoding.UTF8, "application/json"));
           
            Assert.True(handleRequest.StatusCode == System.Net.HttpStatusCode.Created);

            var response = await httpClient.GetAsync("http://localhost:44382/lead/1");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<LeadDTO>(jsonContent);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Anna", result.FirstName);
            Assert.AreEqual("Konovalova", result.LastName);
            Assert.AreEqual("Ivanovna", result.Patronymic);
            Assert.AreEqual("09.09.1989 0:00:00", result.BirthDate);
        }
    }
}