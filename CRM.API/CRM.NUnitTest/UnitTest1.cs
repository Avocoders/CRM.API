using NUnit.Framework;
using System.Net.Http;
using CRM.API;
using CRM.API.Models.Input;
using Newtonsoft.Json;
using System.Text;
using CRM.API.Models.Output;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Autofac.Extensions.DependencyInjection;
using TransactionStore.API.Models.Input;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CRM.API.Configuration;

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
        public async Task CreateTransferTest()
        {
            var transferInputModel = new TransferInputModel()
            {
                AccountId = 256,
                CurrencyId = 2,
                Amount = 80,
                AccountIdReceiver = 555
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transferInputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/transfer", jsonContent);
            string ids = Convert.ToString(await response.Content.ReadAsStringAsync());
            string[] data = Regex.Split(ids, @"\D+");
            long id = Convert.ToInt64(data[1]);
            string result = await client.GetStringAsync(LocalHost.localHostTransaction + $"transaction/{id}");
            var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(result)[0];
            Assert.AreEqual(actual.AccountId, 256);
            Assert.AreEqual(actual.Amount, 80);
            Assert.AreEqual(actual.Type, "Transfer");
        }

        [TestCase(256)]
        [TestCase(2)]
        public async Task GetLeadTest(int num)
        {
            string result = await client.GetStringAsync(LocalHost.localHostCrm + $"lead/{num}");
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
            LeadOutputMock test = new LeadOutputMock();
            LeadOutputModel expected = test.GetLeadByIdMock(num);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetLeadWithUpdatedEmail()
        {
            var inputmodel = new EmailInputModel()
            {
                Id = 256,
                Email = "malyshevvictor623@gmail.com"
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "lead/email", jsonContent);
            string actual = Convert.ToString(await response.Content.ReadAsStringAsync());
            var expected = "User with this email already exists";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task CreateDepositTest()
        {
            var transactionInputModel = new TransactionInputModel()
            {
                AccountId = 256,
                CurrencyId = 2,
                Amount = 80
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionInputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/deposit", jsonContent);
            long id = Convert.ToInt64(await response.Content.ReadAsStringAsync());
            string result = await client.GetStringAsync(LocalHost.localHostTransaction + $"transaction/{id}");
            var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(result)[0];
            Assert.AreEqual(actual.AccountId, 256);
            Assert.AreEqual(actual.Amount, 80);
            Assert.AreEqual(actual.Type, "Deposit");
        }

        [Test]
        public async Task CreateWithdrawTest()
        {
            var transactionInputModel = new TransactionInputModel()
            {
                AccountId = 256,
                CurrencyId = 1,
                Amount = 10
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionInputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/withdraw", jsonContent);
            long id = Convert.ToInt64(await response.Content.ReadAsStringAsync());
            string result = await client.GetStringAsync(LocalHost.localHostTransaction + $"transaction/{id}");
            var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(result)[0];
            Assert.AreEqual(actual.AccountId, 256);
            Assert.AreEqual(actual.Amount, -10);
            Assert.AreEqual(actual.Type, "Withdraw");
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            server.Dispose();
            client.Dispose();
        }
    }
}

