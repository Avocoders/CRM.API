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

namespace CRM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILeadRepository _repo;
        public TransactionController(ILeadRepository repo)
        {
            _httpClient = new HttpClient();
            _repo = repo;
        }
        
        /// <summary>
        /// refers to TransactionStore to create a transfer transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("transfer")]
        public async Task<ActionResult<List<long>>> CreateTransferTransaction([FromBody] TransferInputModel transactionModel)
        {
         //   if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
          //  if (_repo.GetAccountById(transactionModel.AccountIdReceiver).Data is null) return BadRequest("The account of receiver is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            transactionModel.CurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountId).Data;
            transactionModel.ReceiverCurrencyId = _repo.GetCurrencyByAccountId(transactionModel.AccountIdReceiver).Data;
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(LocalHost.localHostTransaction + "transaction/transfer", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        /// <summary>
        /// refers to TransactionStore to create a withdraw transaction
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("withdraw")]
        public async Task<ActionResult<long>> CreateWithdrawTransaction([FromBody] TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId) is null) return BadRequest("The user is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
          
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(LocalHost.localHostTransaction+ "transaction/withdraw", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
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
            if (_repo.GetAccountById(transactionModel.AccountId) is null) return BadRequest("The user is not found");
            if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transactionModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(LocalHost.localHostTransaction+"transaction/deposit", jsonContent);
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        /// <summary>
        /// refers to TransactionStore to get all lead's transactions by leadId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("by-account-id/{accountId}")]
        public async Task<ActionResult<List<TransferOutputModel>>> GetTransactionsByAccountId(long accountId)
        {
            
            if (accountId <= 0) return BadRequest("Account was not found");
            var response = await _httpClient.GetAsync(LocalHost.localHostTransaction+$"transaction/by-account-id/{accountId}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        /// <summary>
        /// refers to TransactionStore to get transaction by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionOutputModel>> GetTransactionById(long id)
        {
            if (id <= 0) return BadRequest("Transaction was not found");
            var response = await _httpClient.GetAsync(LocalHost.localHostTransaction + $"transaction/{id}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        /// <summary>
        /// refers to TransactionStore to get balance by leadId and currencyId
        /// </summary>
        /// <param name="leadId"></param>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{leadId}/balance/{currencyId}")]
        public async Task<ActionResult<decimal>> GetBalanceByAccountIdInCurrency(long leadId, byte currencyId)
        {
            if (leadId <= 0) return BadRequest("Lead was not found");
            if (currencyId <= 0) return BadRequest("Currency was not found");
            var response = await _httpClient.GetAsync(LocalHost.localHostTransaction + $"transaction/{leadId}/balance/{currencyId}");
            string content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }
    }
}
