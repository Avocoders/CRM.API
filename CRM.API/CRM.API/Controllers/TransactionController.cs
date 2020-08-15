using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;
using Newtonsoft.Json;
using CRM.Data;
using CRM.API.Models.Output;
using Microsoft.AspNetCore.Http;
using CRM.API.Configuration;
using RestSharp;

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly RestClient _restClient;
        private readonly ILeadRepository _repo;
        public TransactionController(ILeadRepository repo)
        {
            _httpClient = new HttpClient();
            _restClient = new RestClient();
            _repo = repo;
        }
        
        /// <summary>
        /// refers to TransactionStore to create a transfer transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       //контроллер не возвращает badRequest из TransactionStore
        [HttpPost("transfer")]
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
            if (_repo.GetAccountById(transactionModel.AccountIdReceiver).Data is null) return BadRequest("The account of receiver is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
            transactionModel.ReceiverCurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountIdReceiver).Data;
            var restClient = new RestClient("https://localhost:44388/");
            var restRequest = new RestRequest("transaction/transfer", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            restRequest.OnBeforeDeserialization = r => { r.ContentType = "application/json"; }; 
            return restClient.Execute<List<long>>(restRequest).Data; 
        }

        /// <summary>
        /// refers to TransactionStore to create a withdraw transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       //контроллер не возвращает badRequest из TransactionStore (not enough money)
        [HttpPost("withdraw")]
        public async Task<ActionResult<long>> CreateWithdrawTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
            var restClient = new RestClient("https://localhost:44388/");
            var restRequest = new RestRequest("transaction/withdraw", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            restRequest.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };
            return restClient.Execute<long>(restRequest).Data;
        }

        /// <summary>
        /// refers to TransactionStore to create a deposit transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("deposit")]
        public async Task<ActionResult<long>> CreateDepositTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
            var restClient = new RestClient("https://localhost:44388/");
            var restRequest = new RestRequest("transaction/deposit", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(transactionModel);
            restRequest.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };
            return restClient.Execute<long>(restRequest).Data; ;
        }

        /// <summary>
        /// refers to TransactionStore to get all lead's transactions by leadId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("by-account-id/{accountId}")]
        public async Task<ActionResult<List<TransactionOutputModel>>> GetTransactionsByAccountId(long accountId)
        {
            if (accountId <= 0) return BadRequest("Account was not found");
            var restClient = new RestClient("https://localhost:44388/");
            var restRequest = new RestRequest($"transaction/by-account-id/{accountId}", Method.GET, DataFormat.Json);
            return restClient.Execute<List<TransactionOutputModel>>(restRequest).Data;
        }

        /// <summary>
        /// refers to TransactionStore to get transaction by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TransactionOutputModel>>> GetTransactionById(long id)
        {
            if (id <= 0) return BadRequest("Transaction was not found");
            var restClient = new RestClient("https://localhost:44388/");
            var restRequest = new RestRequest($"transaction/{id}", Method.GET, DataFormat.Json);
            var a = restClient.Execute<List<TransactionOutputModel>>(restRequest).Data;
            return a;
        }

    /// <summary>
    /// refers to TransactionStore to get balance by leadId and currencyId
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{accountId}/balance")]
        public async Task<ActionResult<decimal>> GetBalanceByAccountIdInCurrency(long accountId)
        {
            if (accountId <= 0) return BadRequest("Account was not found");
            var restClient = new RestClient("https://localhost:44388/");
            var restRequest = new RestRequest($"transaction/{accountId}/balance", Method.GET, DataFormat.Json);
            return restClient.Execute<decimal>(restRequest).Data;
        }
    }
}
