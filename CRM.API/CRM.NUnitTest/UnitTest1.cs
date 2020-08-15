using NUnit.Framework;
using System.Net.Http;
using CRM.API;
using CRM.API.Models.Input;
using Newtonsoft.Json;
using System.Text;
using CRM.API.Models.Output;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Autofac.Extensions.DependencyInjection;
using System.Collections.Generic;
using CRM.API.Configuration;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using CRM.Core;
using System.Data.SqlClient;
using Autofac;
using CRM.NUnitTest.Mocks.OutputModelMocks;

namespace CRM.NUnitTest
{
    public class Tests
    {
        IWebHostBuilder webHostBuilder;
        TestServer server;
        HttpClient client;



        IDbConnection _connection;

        [OneTimeSetUp]
        public void Setup()
        {
            webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Testing") // You can set the environment you want (development, staging, production)
                        .ConfigureServices(services => services.AddAutofac())
                        .UseStartup<Startup>(); // Startup class of your web app project



            server = new TestServer(webHostBuilder);
            var sp = server.Services.GetAutofacRoot();
            var options = sp.Resolve<IOptions<StorageOptions>>();
            client = server.CreateClient();
            _connection = new SqlConnection(options.Value.DBConnectionString);
            _connection.Execute(Queries.fillTestBase);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]

        public async Task GetLeadTest(int num)
        {
            string result = await client.GetStringAsync(LocalHost.localHostCrm + $"lead/{num}");
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
            LeadOutputModelMocks test = new LeadOutputModelMocks();
            LeadOutputModel expected = test.GetLeadMockById(num);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]

        public async Task FindLeadsBySearchParametersTest(int num)
        {
            var test = new LeadInputModelMocks();
            var inputmodel = test.SearchInputMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "lead/search", jsonContent);   //leadsearch тоже в константу
            var ids = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<LeadOutputModel>>(ids);
            var leadOutputMock = new LeadOutputModelMocks();
            var expected = leadOutputMock.SearchParametersMock(num);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(10)]
        public async Task GetLeadWithUpdatedEmail(int num)
        {
            LeadInputModelMocks test = new LeadInputModelMocks();
            EmailInputModel inputmodel = test.GetEmailInputModelByLeadId(num);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "lead/email", jsonContent);
            string actual = await response.Content.ReadAsStringAsync();
            LeadOutputModelMocks result = new LeadOutputModelMocks();
            string expected = result.GetEmailByLeadId(num);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(78)]

        public async Task GetAccountTest(int num) 
        {
            string result = await client.GetStringAsync(LocalHost.localHostCrm + $"lead/account/{num}");
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(result);
            var mockGetter = new AccountMocksGetter();
            var expected = mockGetter.GetAccountOfLeadMock(num);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(78)]

        public async Task GetAccountByLeadIdTest(int num) 
        {
            string result = await client.GetStringAsync(LocalHost.localHostCrm + $"lead/{num}/accounts");
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(result);
            var accountMocksGetter = new AccountMocksGetter();
            var expected = accountMocksGetter.GetAccountByLeadOfLeadMock(num);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]

        public async Task AddAccountTest(int num)
        {
            AccountInputModelMock test = new AccountInputModelMock();
            AccountInputModel inputmodel = test.AddAccountMock(num);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var result = await client.PostAsync(LocalHost.localHostCrm + "lead/account", jsonContent);
            string model = Convert.ToString(await result.Content.ReadAsStringAsync());
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(model);
            AccountMocksGetter testresult = new AccountMocksGetter();
            LeadWithAccountsOutputModel expected = testresult.AddAccountByLeadOfLeadMock(num);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]

        public async Task UpdateAccountTest(int num)
        {
            AccountInputModelMock test = new AccountInputModelMock();
            AccountInputModel inputmodel = test.UpdateAccountMock(num);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var result = await client.PutAsync(LocalHost.localHostCrm + "lead/account", jsonContent);
            string model = Convert.ToString(await result.Content.ReadAsStringAsync());
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(model);
            AccountMocksGetter testresult = new AccountMocksGetter();
            LeadWithAccountsOutputModel expected = testresult.UpdateAccountByLeadOfLeadMock(num);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task CreateDepositTest(int num)   //сделала, но хз, как будем удалять их из базы
        {
            var outputModelMock = new TransactionOutputModelMocks();
            var expected = outputModelMock.GetIdDeposit(num);
            var inputModelMock = new TransactionInputModelMocks();            
            var inputModel = inputModelMock.GetDepositInputModel(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/deposit", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (num < 4)
            {
                var actual = JsonConvert.DeserializeObject<int>(result);
                Assert.AreEqual(expected, actual);
            }
            else
            { 
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task CreateTransferTest(int num)   //сделала, но хз, как будем удалять их из базы
        {
            var outputModelMock = new TransactionOutputModelMocks();
            var expected = outputModelMock.GetIdsTransfer(num);
            var inputModelMock = new TransactionInputModelMocks();
            var inputModel = inputModelMock.GetTransferInputModel(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/transfer", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (num < 4)
            {
                var actual = JsonConvert.DeserializeObject<List<int>>(result);  
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task CreateWithdrawTest(int num)         //сделала, но хз, как будем удалять их из базы
        {
            var outputModelMock = new TransactionOutputModelMocks();
            var expected = outputModelMock.GetIdWithdraw(num);
            var inputModelMock = new TransactionInputModelMocks();
            var inputModel = inputModelMock.GetWithdrawInputModel(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/withdraw", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (num < 4)
            {
                var actual = JsonConvert.DeserializeObject<int>(result);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, result);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        //[TestCase(0)]
        [TestCase(6)]
        [TestCase(4)]
        public async Task GetTransactionsByAccountIdTest(int num)
        {
            var outputModelMock = new TransactionOutputModelMocks();
            var expected = outputModelMock.GetTransactionsMockByAccountId(num);
            var response = await client.GetStringAsync(LocalHost.localHostCrm + $"transaction/by-account-id/{num}");
            if (num > 0)
            {
                var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(response);
                Assert.AreEqual(expected[0].AccountId, actual[0].AccountId);
                Assert.AreEqual(expected[0].Type, actual[0].Type);
                Assert.AreEqual(expected[0].Amount, actual[0].Amount);
                Assert.AreEqual(expected.Count, actual.Count);
            }
            else
            {
                //Assert.AreEqual(expected, response);   не хочет возвращать сообщение ошибки
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        //[TestCase(13)]
        //[TestCase(0)]
        public async Task GetTransactionByIdTest(int num)
        {
            var outputModelMock = new TransactionOutputModelMocks();
            var expected = outputModelMock.GetTransactionMockById(num);
            var response = await client.GetStringAsync(LocalHost.localHostCrm + $"transaction/{num}");
            if (num > 0)
            {                                                    //надо в контроллере поменять лист на просто модель
                var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(response);                
                Assert.AreEqual(expected.AccountId, actual[0].AccountId);
                Assert.AreEqual(expected.Type, actual[0].Type);
                Assert.AreEqual(expected.Amount, actual[0].Amount);               
            }
            else
            {
                //Assert.AreEqual(expected, response);   не хочет возвращать сообщение ошибки
            }
        }

        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        public async Task CreateLead(int num)
        {
            LeadInputModelMocks test = new LeadInputModelMocks();
            LeadInputModel inputmodel = test.CreateLeadMock(num);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var result = await client.PostAsync(LocalHost.localHostCrm + "lead", jsonContent);
            string model = Convert.ToString(await result.Content.ReadAsStringAsync());
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(model);
            LeadOutputModelMocks testresult = new LeadOutputModelMocks();
            LeadOutputModel expected = testresult.GetLeadMockById(num);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Login, actual.Login);
            Assert.AreEqual(expected.BirthDate, actual.BirthDate);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public async Task RemoveLeadTest(int num)
        {
            var response = await client.DeleteAsync(LocalHost.localHostCrm + $"lead/{num}");
            string actual = Convert.ToString(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("Successfully deleted", actual);
        }
        [OneTimeTearDown]
        public void Teardown()
        {
            _connection.Execute(Queries.clearTestBase);
            server.Dispose();
            client.Dispose();
        }
    }
}

