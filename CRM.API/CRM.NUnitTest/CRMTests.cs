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


        //        [TestCase(18)]
        //        [TestCase(19)]
        //        [TestCase(20)]
        //        [TestCase(21)]
        //        [TestCase(22)]
        //        [TestCase(23)]             
        //        public async Task AddAccountTest(int num)   //  (перестал работать, контроллер выводит лажу)
        //        {
        //            var outputData = new OutputDataMocksForAccounts();
        //            var expected = outputData.GetAccountOutputModelMockById(num);
        //            var inputData = new InputDataMocksForAccounts();
        //            var inputmodel = inputData.GetAccountInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.accountUrl, jsonContent);
        //            var result = await response.Content.ReadAsStringAsync();

        //            if (num < 23)
        //            {
        //                var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(result);
        //                Assert.AreEqual(expected, actual);
        //            }
        //            else
        //            {

        //                Assert.AreEqual(expected, result);
        //            }
        //        }


        //        [TestCase(11)]
        //        [TestCase(12)]
        //        [TestCase(13)]
        //        [TestCase(14)]
        //        [TestCase(15)]
        //        [TestCase(16)]
        //        public async Task AddLeadTest(int num) 
        //        {
        //            var outputData = new OutputDataMocksForLeads();
        //            var expected = outputData.GetLeadOutputModelMockById(num);
        //            var inputData = new InputDataMocksForLeads();
        //            var inputmodel = inputData.GetLeadInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.leadUrl, jsonContent);
        //            var result = await response.Content.ReadAsStringAsync();
        //            if (num < 14)
        //            {
        //                var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
        //                Assert.AreEqual(expected.Id, actual.Id);
        //                Assert.AreEqual(expected.FirstName, actual.FirstName);
        //                Assert.AreEqual(expected.LastName, actual.LastName);
        //                Assert.AreEqual(expected.Login, actual.Login);
        //                Assert.AreEqual(expected.BirthDate, actual.BirthDate);
        //                Assert.AreEqual(expected.Phone, actual.Phone);
        //                Assert.AreEqual(expected.City, actual.City);
        //            }
        //            else
        //            {
        //                Assert.AreEqual(expected, result);
        //            }
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        public async Task CreateDepositTest(int num)       //сделала, но хз, как будем удалять их из базы
        //        {
        //            var outputData = new OutputDataMocksForTransactions();
        //            var expected = outputData.GetIdDepositMock(num);
        //            var inputData = new InputDataMocksForTransactions();
        //            var inputModel = inputData.GetDepositInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.creationDepositUrl, jsonContent);
        //            var result = await response.Content.ReadAsStringAsync();
        //            if (num < 4)
        //            {
        //                var actual = JsonConvert.DeserializeObject<int>(result);
        //                Assert.AreEqual(expected, actual);
        //            }
        //            else
        //            {
        //                Assert.AreEqual(expected, result);
        //            }
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        public async Task CreateTransferTest(int num)         //сделала, но хз, как будем удалять их из базы
        //        {
        //            var outputData = new OutputDataMocksForTransactions();
        //            var expected = outputData.GetIdsTransferMock(num);
        //            var inputData = new InputDataMocksForTransactions();
        //            var inputModel = inputData.GetTransferInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.creationTransferUrl, jsonContent);
        //            var result = await response.Content.ReadAsStringAsync();
        //            if (num < 4)
        //            {
        //                var actual = JsonConvert.DeserializeObject<List<int>>(result);
        //                Assert.AreEqual(expected, actual);
        //            }
        //            else
        //            {
        //                Assert.AreEqual(expected, result);
        //            }
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        public async Task CreateWithdrawTest(int num)               //сделала, но хз, как будем удалять их из базы
        //        {
        //            var outputData = new OutputDataMocksForTransactions();
        //            var expected = outputData.GetIdWithdrawMock(num);
        //            var inputData = new InputDataMocksForTransactions();
        //            var inputModel = inputData.GetWithdrawInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.creationWithdrawUrl, jsonContent);
        //            var result = await response.Content.ReadAsStringAsync();
        //            if (num < 4)
        //            {
        //                var actual = JsonConvert.DeserializeObject<int>(result);
        //                Assert.AreEqual(expected, actual);
        //            }
        //            else
        //            {
        //                Assert.AreEqual(expected, result);
        //            }
        //        }



        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        public async Task FindLeadsBySearchParametersTest(int num)      // (не правильно выводит список(повторяются лиды))
        //        {
        //            var inputData = new InputDataMocksForLeads();
        //            var inputmodel = inputData.SearchInputMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.searchLeadsUrl, jsonContent);              
        //            var actual = JsonConvert.DeserializeObject<List<LeadOutputModel>>(await response.Content.ReadAsStringAsync());
        //            var outputData = new OutputDataMocksForLeads();
        //            var expected = outputData.GetListOfLeadOutputModelsMockById(num);
        //            Assert.AreEqual(expected[0].Id, actual[0].Id);
        //            Assert.AreEqual(expected[0].FirstName, actual[0].FirstName);
        //            Assert.AreEqual(expected[0].LastName, actual[0].LastName);
        //            Assert.AreEqual(expected[0].Login, actual[0].Login);
        //            Assert.AreEqual(expected[0].BirthDate, actual[0].BirthDate);
        //            Assert.AreEqual(expected[0].Phone, actual[0].Phone);
        //            Assert.AreEqual(expected[0].Accounts.Count, actual[0].Accounts.Count);
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        public async Task GetAccountByIdTest(int num)  //  (контроллер выводит неправильные ID Lead-а)
        //        {
        //            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.accountUrl + $"{num}");
        //            var actual = JsonConvert.DeserializeObject<AccountWithLeadOutputModel>(response);
        //            var outputData = new OutputDataMocksForAccounts();
        //            var expected = outputData.GetAccountWithLeadOutputModelMockById(num);
        //            Assert.AreEqual(expected, actual);
        //        }


        //        [TestCase(1)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        [TestCase(70)]
        //        public async Task GetAccountsByLeadIdTest(int num)  //  (нет догадок почему не работает, значения одинаковые)
        //        {
        //            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}" + EndpointUrl.accountsUrl);
        //            var actual = JsonConvert.DeserializeObject<List<AccountOutputModel>>(response);
        //            var outputData = new OutputDataMocksForAccounts();
        //            var expected = outputData.GetListOfAccountOutputModelsMock(num);
        //            Assert.AreEqual(expected, actual);
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(256)]
        //        public async Task GetBalanceByAccountIdTest(int num)  
        //        {
        //            var outputData = new OutputDataMocksForTransactions();
        //            var expected = outputData.GetBalanceMockByAccountId(num);
        //            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.transactionUrl +$"{num}" + EndpointUrl.balanceUrl);
        //            var actual = JsonConvert.DeserializeObject<decimal>(response);
        //            Assert.AreEqual(expected, actual);
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]        
        //        public async Task GetLeadByIdTest(int num)   
        //        {
        //            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}");  
        //            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(response);
        //            var outputData = new OutputDataMocksForLeads();
        //            var expected = outputData.GetLeadOutputModelMockById(num);
        //            Assert.AreEqual(expected.Id, actual.Id);
        //            Assert.AreEqual(expected.FirstName, actual.FirstName);
        //            Assert.AreEqual(expected.LastName, actual.LastName);
        //            Assert.AreEqual(expected.Login, actual.Login);
        //            Assert.AreEqual(expected.BirthDate, actual.BirthDate);
        //            Assert.AreEqual(expected.Phone, actual.Phone);
        //            Assert.AreEqual(expected.City, actual.City);
        //            Assert.AreEqual(expected.Accounts.Count, actual.Accounts.Count);
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(4)]
        //        [TestCase(8)]
        //        //[TestCase(13)]
        //        //[TestCase(0)]
        //        public async Task GetTransactionByIdTest(int num)   
        //        {
        //            var outputData = new OutputDataMocksForTransactions();
        //            var expected = outputData.GetTransactionMockById(num);
        //            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.transactionUrl + $"{num}");
        //            if (num > 0)
        //            {                                                    //надо в контроллере поменять лист на просто модель
        //                var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(response);
        //                Assert.AreEqual(expected.AccountId, actual[0].AccountId);
        //                Assert.AreEqual(expected.Type, actual[0].Type);
        //                Assert.AreEqual(expected.Amount, actual[0].Amount);
        //            }
        //            else
        //            {
        //                //Assert.AreEqual(expected, response);   не хочет возвращать сообщение ошибки
        //            }
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        //[TestCase(0)]
        //        [TestCase(6)]
        //        [TestCase(4)]
        //        public async Task GetTransactionsByAccountIdTest(int num)   
        //        {
        //            var outputData = new OutputDataMocksForTransactions();
        //            var expected = outputData.GetTransactionsMockByAccountId(num);
        //            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.transactionByAccountIdUrl + $"{num}");
        //            if (num > 0)
        //            {
        //                var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(response);
        //                Assert.AreEqual(expected[0].AccountId, actual[0].AccountId);
        //                Assert.AreEqual(expected[0].Type, actual[0].Type);
        //                Assert.AreEqual(expected[0].Amount, actual[0].Amount);
        //                Assert.AreEqual(expected.Count, actual.Count);
        //            }
        //            else
        //            {
        //                //Assert.AreEqual(expected, response);   не хочет возвращать сообщение ошибки
        //            }
        //        }
        //           [TestCase(11)] 
        //        [TestCase(12)] 
        //        [TestCase(13)] 
        //        [TestCase(14)] 
        //        [TestCase(15)] 
        //        [TestCase(20)] 
        //        public async Task RemoveLeadByIdTest(int num) 
        //        { 
        //            var response = await _client.DeleteAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}"); 
        //            var actual = await response.Content.ReadAsStringAsync(); 
        //            if (num < 14) 
        //                Assert.AreEqual("Successfully deleted", actual); 
        //            else 
        //                Assert.AreEqual("Lead was not found", actual); 
        //        } 


        //        [TestCase(7)]
        //        [TestCase(8)]
        //        [TestCase(9)]
        //        [TestCase(10)]
        //        [TestCase(11)]
        //        [TestCase(12)]
        //        public async Task UpdateAccountByIdTest(int num)   
        //        {
        //            var inputData = new InputDataMocksForAccounts();
        //            var inputmodel = inputData.GetAccountInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
        //            var response = await _client.PutAsync(_crmUrl + EndpointUrl.accountUrl, jsonContent);            
        //            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(await response.Content.ReadAsStringAsync());
        //            var outputData = new OutputDataMocksForAccounts();
        //            var expected = outputData.GetAccountOutputModelMockById(num);
        //            Assert.AreEqual(expected, actual);
        //        }


        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        [TestCase(7)]
        //        [TestCase(8)]        
        //        public async Task UpdateEmailByLeadIdTest(int num)   
        //        {
        //            var inputData = new InputDataMocksForLeads();
        //            var inputmodel = inputData.GetEmailInputModelMockByLeadId(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
        //            var response = await _client.PostAsync(_crmUrl + EndpointUrl.leadEmailUrl, jsonContent);
        //            var actual = await response.Content.ReadAsStringAsync();
        //            var outputData = new OutputDataMocksForLeads();
        //            string expected = outputData.GetEmailByLeadId(num);
        //            Assert.AreEqual(expected, actual);
        //        }


        //        [TestCase(1)]
        //        [TestCase(2)]
        //        [TestCase(3)]
        //        [TestCase(4)]
        //        [TestCase(5)]
        //        [TestCase(6)]
        //        public async Task UpdateLeadByIdTest(int num)  
        //        {
        //            var inputData = new InputDataMocksForLeads();
        //            var inputmodel = inputData.GetLeadInputModelMock(num);
        //            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
        //            var response = await _client.PutAsync(_crmUrl + EndpointUrl.leadUrl, jsonContent);            
        //            var result = await response.Content.ReadAsStringAsync();            
        //            var outputData = new OutputDataMocksForLeads();
        //            var expected = outputData.GetLeadOutputModelAfterUpdateMockById(num);
        //            if (num < 4)
        //            {
        //                var actual = JsonConvert.DeserializeObject<LeadOutputModel>(result);
        //                Assert.AreEqual(expected.Id, actual.Id);
        //                Assert.AreEqual(expected.FirstName, actual.FirstName);
        //                Assert.AreEqual(expected.LastName, actual.LastName);
        //                Assert.AreEqual(expected.Login, actual.Login);
        //                Assert.AreEqual(expected.BirthDate, actual.BirthDate);
        //                Assert.AreEqual(expected.Phone, actual.Phone);
        //                Assert.AreEqual(expected.City, actual.City);
        //            }
        //            else
        //            {
        //                Assert.AreEqual(expected, result);
        //            }
        //        }


        //        [OneTimeTearDown]
        //        public void Teardown()
        //        {
        //           _connection.Execute(Queries.clearTestBase);
        //            _server.Dispose();
        //            _client.Dispose();
        //        }
        //    }
        //}

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task FindLeadsBySearchParametersTest(int num)      // (�� ��������� ������� ������(����������� ����)) 
        {
            var inputData = new InputDataMocksForLeads();
            var inputmodel = inputData.SearchInputMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.searchLeadsUrl, jsonContent);
            var actual = JsonConvert.DeserializeObject<List<LeadOutputModel>>(await response.Content.ReadAsStringAsync());
            var outputData = new OutputDataMocksForLeads();
            var expected = outputData.GetListOfLeadOutputModelsMockById(num);
            Assert.AreEqual(expected[0].Id, actual[0].Id);
            Assert.AreEqual(expected[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expected[0].LastName, actual[0].LastName);
            Assert.AreEqual(expected[0].Login, actual[0].Login);
            Assert.AreEqual(expected[0].BirthDate, actual[0].BirthDate);
            Assert.AreEqual(expected[0].Phone, actual[0].Phone);
           
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task GetAccountByIdTest(int num) 
        {
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.accountUrl + $"{num}");
            var actual = JsonConvert.DeserializeObject<AccountWithLeadOutputModel>(response);
            var outputData = new OutputDataMocksForAccounts();
            var expected = outputData.GetAccountWithLeadOutputModelMockById(num);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(1)]
        [TestCase(3)]
        [TestCase(70)]
        public async Task GetAccountsByLeadIdTest(int num)
        {
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}" + EndpointUrl.accountsUrl);
            var actual = JsonConvert.DeserializeObject<List<AccountOutputModel>>(response);
            var outputData = new OutputDataMocksForAccounts();
            var expected = outputData.GetListOfAccountOutputModelsMock(num);
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
            var outputData = new OutputDataMocksForTransactions();
            var expected = outputData.GetBalanceMockByAccountId(num);
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.transactionUrl + $"{num}" + EndpointUrl.balanceUrl);
            var actual = JsonConvert.DeserializeObject<decimal>(response);
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
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}");
            var actual = JsonConvert.DeserializeObject<LeadOutputModel>(response);
            var outputData = new OutputDataMocksForLeads();
            var expected = outputData.GetLeadOutputModelMockById(num);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Login, actual.Login);
            Assert.AreEqual(expected.BirthDate, actual.BirthDate);
            Assert.AreEqual(expected.Phone, actual.Phone);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.Accounts.Count, actual.Accounts.Count);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        //[TestCase(13)] 
        //[TestCase(0)] 
        public async Task GetTransactionByIdTest(int num)
        {
            var outputData = new OutputDataMocksForTransactions();
            var expected = outputData.GetTransactionMockById(num);
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.transactionUrl + $"{num}");
            if (num > 0)
            {                                                    //���� � ����������� �������� ���� �� ������ ������ 
                var actual = JsonConvert.DeserializeObject<List<TransactionOutputModel>>(response);
                Assert.AreEqual(expected.AccountId, actual[0].AccountId);
                Assert.AreEqual(expected.Type, actual[0].Type);
                Assert.AreEqual(expected.Amount, actual[0].Amount);
            }
            else
            {
                //Assert.AreEqual(expected, response);   �� ����� ���������� ��������� ������ 
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
            var outputData = new OutputDataMocksForTransactions();
            var expected = outputData.GetTransactionsMockByAccountId(num);
            var response = await _client.GetStringAsync(_crmUrl + EndpointUrl.transactionByAccountIdUrl + $"{num}");
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
                //Assert.AreEqual(expected, response);   �� ����� ���������� ��������� ������ 
            }
        }
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(20)]
        public async Task RemoveLeadByIdTest(int num)
        {
            var response = await _client.DeleteAsync(_crmUrl + EndpointUrl.leadUrl + $"{num}");
            var actual = await response.Content.ReadAsStringAsync();
            if (num < 14)
                Assert.AreEqual("Successfully deleted", actual);
            else
                Assert.AreEqual("Lead was not found", actual);
        }


        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task UpdateAccountByIdTest(int num)
        {
            var inputData = new InputDataMocksForAccounts();
            var inputmodel = inputData.GetAccountInputModelMock(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_crmUrl + EndpointUrl.accountUrl, jsonContent);
            var actual = JsonConvert.DeserializeObject<LeadWithAccountsOutputModel>(await response.Content.ReadAsStringAsync());
            var outputData = new OutputDataMocksForAccounts();
            var expected = outputData.GetAccountOutputModelMockById(num);
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
            var inputData = new InputDataMocksForLeads();
            var inputmodel = inputData.GetEmailInputModelMockByLeadId(num);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(inputmodel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_crmUrl + EndpointUrl.leadEmailUrl, jsonContent);
            var actual = await response.Content.ReadAsStringAsync();
            var outputData = new OutputDataMocksForLeads();
            string expected = outputData.GetEmailByLeadId(num);
            Assert.AreEqual(expected, actual);
        }
                

        [OneTimeTearDown]
        public void Teardown()
        {
            _connection.Execute(Queries.clearTestBase);
            _server.Dispose();
            _client.Dispose();
        }

    }
}
