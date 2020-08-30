using NUnit.Framework;
using System.Net.Http;
using CRM.API;
using Newtonsoft.Json;
using System.Text;
using CRM.API.Models.Output;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Autofac.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using CRM.Core;
using System.Data.SqlClient;
using Autofac;
using CRM.NUnitTest.Mocks.OutputModelMocks;
using System.Net;
using System.Linq;
using CRM.API.Models;

namespace CRM.NUnitTest
{
    public class Tests
    {
        private IWebHostBuilder _webHostBuilder;
        private TestServer _server;
        private HttpClient _client;
        private IDbConnection _connection;
        private string _crmUrl;
        private string _transactionStoreAPIUrl;
        private InputDataMocksForAccounts _inputDataForAccount;
        private InputDataMocksForLeads _inputDataForLead;
        private InputDataMocksForTransactions _inputDataForTransaction;
        private OutputDataMocksForAccounts _outputDataForAccount;
        private OutputDataMocksForLeads _outputDataForLead;
        private OutputDataMocksForTransactions _outputDataForTransaction;

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
            var urlOptions = lifetimeScope.Resolve<IOptions<UrlOptions>>();
            _crmUrl = urlOptions.Value.CrmAPIUrl;
            _transactionStoreAPIUrl = urlOptions.Value.TransactionStoreAPIUrl;
            var databaseOptions = lifetimeScope.Resolve<IOptions<DatabaseOptions>>();
            _connection = new SqlConnection(databaseOptions.Value.DBConnectionString);
            _connection.Execute(Queries.fillTestBase);
            _inputDataForAccount = new InputDataMocksForAccounts();
            _inputDataForLead = new InputDataMocksForLeads();
            _inputDataForTransaction = new InputDataMocksForTransactions();
            _outputDataForAccount = new OutputDataMocksForAccounts();
            _outputDataForLead = new OutputDataMocksForLeads();
            _outputDataForTransaction = new OutputDataMocksForTransactions();   
        }


        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        public async Task AddAccountTest(int num)
        {        
            var expected = _outputDataForAccount.GetAccountWithLeadOutputModelMockById(num);            
            var inputmodel = _inputDataForAccount.GetAccountInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_crmUrl}{EndpointUrl.accountUrl}", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            int failedResult = 23;            
            if (num != failedResult)
            {
                var actual = JsonConvert.DeserializeObject<AccountWithLeadOutputModel>(result);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.AreEqual(expected, result);                      
            }
        }


        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        public async Task AddLeadTest(int num)
        {           
            var expected = _outputDataForLead.GetLeadOutputModelMockById(num);          
            var inputmodel = _inputDataForLead.GetLeadInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_crmUrl}{EndpointUrl.leadUrl}", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            int[] failedResults = new int[] {14, 15, 16 };
            if (failedResults.Contains(num))
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.AreEqual(expected, result);
            }
            else
            {                
                var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
                Assert.AreEqual(expected, actual);               
            }
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task CreateDepositTest(int num) 
        {           
            var expected = _outputDataForTransaction.GetIdDepositMock(num);            
            var inputModel = _inputDataForTransaction.GetDepositInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{ _crmUrl}{ EndpointUrl.creationDepositUrl}", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            int[] failedResults = new int[] { 4, 5, 6 };
            if (failedResults.Contains(num))
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.AreEqual(expected, result);                
            }
            else
            {
                var actual = JsonConvert.DeserializeObject<int>(result);
                Assert.AreEqual(expected, actual);
            }
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task CreateTransferTest(int num)   
        {         
            var expected = _outputDataForTransaction.GetIdsTransferMock(num);          
            var inputModel = _inputDataForTransaction.GetTransferInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_crmUrl}{EndpointUrl.creationTransferUrl}", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            int[] failedResults = new int[] { 4, 5 };
            if (failedResults.Contains(num))
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.AreEqual(expected, result);
            }
            else
            {

                int[] failedResult = new int[] { 6 };
                if (failedResult.Contains(num))
                {
                    Assert.That((int)response.StatusCode, Is.EqualTo(520));

                }
                else
                {
                    var actual = JsonConvert.DeserializeObject<List<int>>(result);
                    Assert.AreEqual(expected, actual);
                }
            }
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task FindLeadsBySearchParametersTest(int num)   
        {           
            var inputmodel = _inputDataForLead.SearchInputMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_crmUrl}{EndpointUrl.searchLeadsUrl}", jsonContent);
            var actual = JsonConvert.DeserializeObject<List<LeadOutputModel>>(await response.Content.ReadAsStringAsync());      
            var expected = _outputDataForLead.GetListOfLeadOutputModelsMockById(num);
            CollectionAssert.AreEqual(expected, actual);                  
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task GetAccountByIdTest(int num) 
        {
            var response = await _client.GetStringAsync($"{_crmUrl}{EndpointUrl.accountUrl}{num}");
            var actual = JsonConvert.DeserializeObject<AccountWithLeadOutputModel>(response);           
            var expected = _outputDataForAccount.GetAccountWithLeadOutputModelMockById(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(1)]
        [TestCase(3)]
        [TestCase(70)]
        public async Task GetAccountsByLeadIdTest(int num)
        {
            var response = await _client.GetStringAsync($"{_crmUrl}{EndpointUrl.leadUrl}{num}{EndpointUrl.accountsUrl}");
            var actual = JsonConvert.DeserializeObject<List<AccountOutputModel>>(response);         
            var expected = _outputDataForAccount.GetListOfAccountOutputModelsMock(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(256)]
        public async Task GetBalanceByAccountIdTest(int num) 
        {           
            var expected = _outputDataForTransaction.GetBalanceMockByAccountId(num);
            var response = await _client.GetStringAsync($"{_crmUrl}{EndpointUrl.transactionUrl}{num}{EndpointUrl.balanceUrl}");
            var actual = JsonConvert.DeserializeObject<BalanceOutputModel>(response);
            int failedResult = 256;
            if (num == failedResult)
            {
                Assert.AreEqual(response, Is.EqualTo(HttpStatusCode.BadRequest));
                
            }
            else
            Assert.AreEqual(expected, actual.Balance);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task GetLeadByIdTest(int num)
        {
            var response = await _client.GetStringAsync($"{_crmUrl}{EndpointUrl.leadUrl}{num}");
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(response);           
            var expected = _outputDataForLead.GetLeadOutputModelMockById(num);
            Assert.AreEqual(expected, actual);            
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(8)]
        public async Task GetTransactionByIdTest(int num)
        {
            var response = await _client.GetStringAsync($"{_crmUrl}{EndpointUrl.transactionUrl}{num}");
            var actual = JsonConvert.DeserializeObject < List<TransactionOutputModel>>(response);
            var expected = _outputDataForTransaction.GetTransactionMockById(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(6)]
        [TestCase(4)]
        public async Task GetTransactionsByAccountIdTest(int num)   
        {            
            var expected = _outputDataForTransaction.GetTransactionsMockByAccountId(num);
            var response = await _client.GetStringAsync($"{_crmUrl}{EndpointUrl.transactionByAccountIdUrl}{num}");
            var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(response);
            CollectionAssert.AreEqual(expected, actual);            
        }


        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(20)]
        public async Task RemoveLeadByIdTest(int num)
        {
            var response = await _client.DeleteAsync($"{_crmUrl}{EndpointUrl.leadUrl}{num}");
            var actual = await response.Content.ReadAsStringAsync();
            int[] failedResults = new int[] { 14, 15, 20 };
            if (failedResults.Contains(num))
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.AreEqual("Lead was not found", actual);
            }
            else
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.AreEqual("Successfully deleted", actual);
            }
        }


        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task UpdateAccountByIdTest(int num)
        {          
            var inputmodel = _inputDataForAccount.GetAccountInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_crmUrl}{EndpointUrl.accountUrl}", jsonContent);
            var actual = JsonConvert.DeserializeObject<AccountWithLeadOutputModel>(await response.Content.ReadAsStringAsync());           
            var expected = _outputDataForAccount.GetAccountWithLeadOutputModelMockById(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task UpdateEmailByLeadIdTest(int num)
        {           
            var inputmodel = _inputDataForLead.GetEmailInputModelMockByLeadId(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_crmUrl}{EndpointUrl.leadEmailUrl}", jsonContent);
            var actual = await response.Content.ReadAsStringAsync();      
            string expected = _outputDataForLead.GetEmailByLeadId(num);
            Assert.AreEqual(expected, actual);
        }
                

        [OneTimeTearDown]
        public void Teardown()
        {
            _client.DeleteAsync($"{_crmUrl}{EndpointUrl.transactionUrl}");
            //_connection.Execute(Queries.clearTestBase);
            _server.Dispose();
            _client.Dispose();
        }
    }
}
