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
using TransactionStore.API.Models.Input;
using System.Collections.Generic;

namespace CRM.NUnitTest
{
    public class Tests
    {
        IWebHostBuilder webHostBuilder;
        TestServer server;
        HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
                        .ConfigureServices(services => services.AddAutofac())
                        .UseStartup<Startup>(); // Startup class of your web app project


            server = new TestServer(webHostBuilder);
            client = server.CreateClient();
        }

        [Test]

        public async Task GetLeadTest()
        {
            string result = await client.GetStringAsync("https://localhost:44382/lead/256");
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
            var expected = new LeadOutputModel()
            {
                Id = 256,
                FirstName = "Viktor",
                LastName = "Malyshev",
                Patronymic = "Grigorievich",
                Login = "ViktorMalyshev5946357064",
                Phone = "+72963050540",
                Email = "ViktorMalyshev5946357064@gmail.com",
                Address = "Malaya Konyushennaya Ulitsa229",
                BirthDate = "1978-05-17 00:00:00",
                RegistrationDate = "2011-07-27 00:00:00",
                ChangeDate = "2020-07-29 21:14:21",
                Role = "Client",
                City = "Gatchina",
                IsDeleted = false
            };

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public async Task CreateDepositTest()
        {
            var transactionInputModel = new TransactionInputModel()
            {
                LeadId = 256,
                CurrencyId = 2,
                Amount = 80
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionInputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44382/transaction/deposit", jsonContent);
            long id = Convert.ToInt64(await response.Content.ReadAsStringAsync());

            string result = await client.GetStringAsync($"https://localhost:44382/transaction/{id}");
            var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(result)[0];

            Assert.AreEqual(actual.LeadId, 256);
            Assert.AreEqual(actual.Currency, "USD");
            Assert.AreEqual(actual.Amount, 80);
            Assert.AreEqual(actual.Type, "Deposit");
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            server.Dispose();
            client.Dispose();
        }
    }
}

