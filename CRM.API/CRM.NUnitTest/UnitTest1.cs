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

        public async Task SearchParametersTest(int num)
        {
            var test = new LeadInputModelMocks();
            var inputmodel = test.SearchInputMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostCrm + "lead/search", jsonContent);   //leadsearch ���� � ���������
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

        public async Task GetAccountTest(int num) //����� ����
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

        public async Task GetAccountByLeadIdTest(int num) // ����� ����
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
        public async Task CreateDepositTest(int num)   //�������, �� ��, ��� ����� ������� �� �� ����
        {
            var inputModelMock = new TransactionInputModelMocks();
            var inputModel = inputModelMock.GetDepositInputModel(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostTransaction + "transaction/deposit", jsonContent);
            var id = await response.Content.ReadAsStringAsync();
            if (num < 4)
            {
                var actual = JsonConvert.DeserializeObject<int>(id);
                var outputModelMock = new TransactionOutputModelMocks();
                var expected = outputModelMock.GetIdDeposit(num);
                Assert.AreEqual(expected, actual);
            }
            else
            {                
                var outputModelMock = new TransactionOutputModelMocks();
                var expected = outputModelMock.GetIdDeposit(num);
                Assert.AreEqual(expected, id);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task CreateTransferTest(int num)   //�������, �� ��, ��� ����� ������� �� �� ����
        {
            var inputModelMock = new TransactionInputModelMocks();
            var inputModel = inputModelMock.GetTransferInputModel(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LocalHost.localHostTransaction + "transaction/transfer", jsonContent);
            var ids = await response.Content.ReadAsStringAsync();
            if (num < 4)
            {
                var actual = JsonConvert.DeserializeObject<List<int>>(ids);
                var outputModelMock = new TransactionOutputModelMocks();
                var expected = outputModelMock.GetIdsTransfer(num);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                var outputModelMock = new TransactionOutputModelMocks();
                var expected = outputModelMock.GetIdsTransfer(num);
                Assert.AreEqual(expected, ids);
            }
        }



            //[Test]
            //public async Task CreateWithdrawTest()
            //{
            //    var transactionInputModel = new TransactionInputModel()
            //    {
            //        AccountId = 256,
            //        CurrencyId = 1,
            //        Amount = 10
            //    };
            //    var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionInputModel), Encoding.UTF8, "application/json");
            //    var response = await client.PostAsync(LocalHost.localHostCrm + "transaction/withdraw", jsonContent);
            //    long id = Convert.ToInt64(await response.Content.ReadAsStringAsync());
            //    string result = await client.GetStringAsync(LocalHost.localHostTransaction + $"transaction/{id}");
            //    var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(result)[0];
            //    Assert.AreEqual(actual.AccountId, 256);
            //    Assert.AreEqual(actual.Amount, -10);
            //    Assert.AreEqual(actual.Type, "Withdraw");
            //}



            //[TestCase(1)]
            //[TestCase(2)]
            //[TestCase(3)]
            //[TestCase(4)]
            //[TestCase(5)]
            //[TestCase(6)]
            //[TestCase(7)]
            //[TestCase(8)]
            //[TestCase(9)]
            //[TestCase(10)]

            //public async Task DeleteLeadTest(int num)
            //{
            //    var response = await client.DeleteAsync(LocalHost.localHostCrm + $"lead/{num}");
            //    string actual = Convert.ToString(await response.Content.ReadAsStringAsync());
            //    Assert.AreEqual("Successfully deleted", actual);
            //}

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

        [OneTimeTearDown]
        public void Teardown()
        {
            _connection.Execute(Queries.clearTestBase);
            server.Dispose();
            client.Dispose();


        }

    }
}

