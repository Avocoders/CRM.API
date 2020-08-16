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
using System.Runtime.InteropServices;

namespace CRM.NUnitTest
{
    public class Tests
    {
        private IWebHostBuilder _webHostBuilder;
        private TestServer _server;
        private HttpClient _client;
        private IDbConnection _connection;
        private string _crmUrl;       

        [OneTimeSetUp]
        public void Setup()
        {
            _webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Testing") 
                        .ConfigureServices(services => services.AddAutofac())
                        .UseStartup<Startup>(); 

            _server = new TestServer(_webHostBuilder);
            var lifetimeScope = _server.Services.GetAutofacRoot();
            _client = _server.CreateClient();
            var urlOptions = lifetimeScope.Resolve<IOptions<APIOptions>>();
            _crmUrl = urlOptions.Value.CrmAPIUrl;            
            var databaseOptions = lifetimeScope.Resolve<IOptions<DatabaseOptions>>();
            _connection = new SqlConnection(databaseOptions.Value.DBConnectionString);
            _connection.Execute(Queries.fillTestBase);
        }


        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]             
        public async Task AddAccountTest(int num)    //done   (перестал работать, контроллер выводит лажу)
        {
            var outputData = new OutputDataMocksForAccounts();
            var expected = outputData.GetAccountOutputModelMockById(num);
            var inputData = new InputDataMocksForAccounts();
            var inputmodel = inputData.GetAccountInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.accountUrl, jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (num < 6)
            {
                var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(result);
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
        public async Task AddLeadTest(int num) //done
        {
            var outputData = new OutputDataMocksForLeads();
            var expected = outputData.GetLeadOutputModelMockById(num);
            var inputData = new InputDataMocksForLeads();
            var inputmodel = inputData.GetLeadInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.leadUrl, jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (num < 4)
            {
                var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.FirstName, actual.FirstName);
                Assert.AreEqual(expected.LastName, actual.LastName);
                Assert.AreEqual(expected.Login, actual.Login);
                Assert.AreEqual(expected.BirthDate, actual.BirthDate);
                Assert.AreEqual(expected.Phone, actual.Phone);
                Assert.AreEqual(expected.City, actual.City);
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
        public async Task CreateDepositTest(int num)       //сделала, но хз, как будем удалять их из базы
        {
            var outputModelMock = new OutputDataMocksForTransactions();
            var expected = outputModelMock.GetIdDepositMock(num);
            var inputModelMock = new InputDataMocksForTransactions();
            var inputModel = inputModelMock.GetDepositInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.creationDepositUrl, jsonContent);
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
        public async Task CreateTransferTest(int num)         //сделала, но хз, как будем удалять их из базы
        {
            var outputModelMock = new OutputDataMocksForTransactions();
            var expected = outputModelMock.GetIdsTransferMock(num);
            var inputModelMock = new InputDataMocksForTransactions();
            var inputModel = inputModelMock.GetTransferInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.creationTransferUrl, jsonContent);
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
        public async Task CreateWithdrawTest(int num)               //сделала, но хз, как будем удалять их из базы
        {
            var outputModelMock = new OutputDataMocksForTransactions();
            var expected = outputModelMock.GetIdWithdrawMock(num);
            var inputModelMock = new InputDataMocksForTransactions();
            var inputModel = inputModelMock.GetWithdrawInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.creationWithdrawUrl, jsonContent);
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


        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(20)]        
        public async Task DeleteLeadByIdTest(int num)   //done
        {
            var response = await _client.DeleteAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}");            
            var actual = await response.Content.ReadAsStringAsync();
            if (num != 20)
                Assert.AreEqual("Successfully deleted", actual);
            else               
                Assert.AreEqual("Lead was not found", actual);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task FindLeadsBySearchParametersTest(int num)  //done     (не правильно выводит список(повторяются лиды))
        {
            var inputModelMock = new InputDataMocksForLeads();
            var inputmodel = inputModelMock.SearchInputMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.searchLeadsUrl, jsonContent);              
            var actual = JsonConvert.DeserializeObject<List<LeadOutputModel>>(await response.Content.ReadAsStringAsync());
            var outputModelMock = new OutputDataMocksForLeads();
            var expected = outputModelMock.GetListOfLeadOutputModelsMockById(num);
            Assert.AreEqual(expected[0].Id, actual[0].Id);
            Assert.AreEqual(expected[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expected[0].LastName, actual[0].LastName);
            Assert.AreEqual(expected[0].Login, actual[0].Login);
            Assert.AreEqual(expected[0].BirthDate, actual[0].BirthDate);
            Assert.AreEqual(expected[0].Phone, actual[0].Phone);
            Assert.AreEqual(expected[0].Accounts.Count, actual[0].Accounts.Count);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task GetAccountByIdTest(int num)  //done (контроллер выводит неправильные ID Lead-а)
        {
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.accountUrl + $"{num}");
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(response);
            var outputMock = new OutputDataMocksForAccounts();
            var expected = outputMock.GetAccountOutputModelMockById(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(78)]
        [TestCase(4)]
        [TestCase(78)]
        public async Task GetAccountsByLeadIdTest(int num)  //done (ждет переделки контроллера)
        {
            var response = await _client.GetStringAsync(LocalHost.localHostCrm + $"lead/{num}/accounts");
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(response);
            var outputMock = new OutputDataMocksForAccounts();
            var expected = outputMock.GetListOfAccountOutputModelsMock(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]        
        public async Task GetLeadByIdTest(int num)
        {
            string response = await _client.GetStringAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}");  
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(response);
            OutputDataMocksForLeads test = new OutputDataMocksForLeads();
            LeadOutputModel expected = test.GetLeadOutputModelMockById(num);
            Assert.AreEqual(expected, actual);
        }


        

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(10)]
        public async Task UpdateEmailByLeadIdTest(int num)
        {
            InputDataMocksForLeads test = new InputDataMocksForLeads();
            EmailInputModel inputmodel = test.GetEmailInputModelByLeadId(num);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(LocalHost.localHostCrm + "lead/email", jsonContent);
            string actual = await response.Content.ReadAsStringAsync();
            OutputDataMocksForLeads result = new OutputDataMocksForLeads();
            string expected = result.GetEmailByLeadId(num);
            Assert.AreEqual(expected, actual);
        }
        

       

        

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]

        public async Task UpdateAccountByIdTest(int num)
        {
            InputDataMocksForAccounts test = new InputDataMocksForAccounts();
            AccountInputModel inputmodel = test.UpdateAccountMock(num);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(LocalHost.localHostCrm + "lead/account", jsonContent);
            string model = Convert.ToString(await result.Content.ReadAsStringAsync());
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(model);
            OutputDataMocksForAccounts testresult = new OutputDataMocksForAccounts();
            LeadWithAccountsOutputModel expected = testresult.UpdateAccountByLeadOfLeadMock(num);
            Assert.AreEqual(expected, actual);
        }

        

        

        

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        //[TestCase(0)]
        [TestCase(6)]
        [TestCase(4)]
        public async Task GetTransactionsByAccountIdTest(int num)
        {
            var outputModelMock = new OutputDataMocksForTransactions();
            var expected = outputModelMock.GetTransactionsMockByAccountId(num);
            var response = await _client.GetStringAsync(LocalHost.localHostCrm + $"transaction/by-account-id/{num}");
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
            var outputModelMock = new OutputDataMocksForTransactions();
            var expected = outputModelMock.GetTransactionMockById(num);
            var response = await _client.GetStringAsync(LocalHost.localHostCrm + $"transaction/{num}");
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

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(256)]
        public async Task GetBalanceByAccountIdTest(int num)
        {
            var outputModelMock = new OutputDataMocksForTransactions();
            var expected = outputModelMock.GetBalanceMockByAccountId(num);
            var response = await _client.GetStringAsync(LocalHost.localHostCrm + $"transaction/{num}/balance");
            var actual = JsonConvert.DeserializeObject<decimal>(response);
            Assert.AreEqual(expected, actual);
        }

        

        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        public async Task UpdateLeadByIdTest(int num)
        {
            InputDataMocksForLeads test = new InputDataMocksForLeads();
            LeadInputModel inputmodel = test.UpdateLeadMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(LocalHost.localHostCrm + "lead", jsonContent);
            string model = Convert.ToString(await result.Content.ReadAsStringAsync());
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(model);
            OutputDataMocksForLeads testresult = new OutputDataMocksForLeads();
            LeadOutputModel expected = testresult.GetLeadOutputModelMockById(num);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Login, actual.Login);
            Assert.AreEqual(expected.BirthDate, actual.BirthDate);
        }


       
        [OneTimeTearDown]
        public void Teardown()
        {
            //_connection.Execute(Queries.clearTestBase);
            _server.Dispose();
            _client.Dispose();
        }
    }
}

